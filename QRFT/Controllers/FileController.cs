using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using QRFT.Model;
using QRFT.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QRTF.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase {
        private static Config config = Config.GetInstance();
        private string filePath;
        private string hash;

        public FileController() {
            filePath = config.FilePath;
            hash = config.Hash;
        }

        [HttpGet("{hash}")]
        public IActionResult Get(string hash) {
            //Logger.ConsoleRouterLog("Post", "/api/file/upload", DateTime.Now);
            // send file from computer
            if (!hash.Equals(this.hash)) {
                return NotFound();
            }

            var stream = System.IO.File.OpenRead(filePath);

            if (stream == null) {
                return NotFound();
            }

            //Response.Headers.Add("Content-Disposition","")

            return new FileStreamResult(stream, "application/octet-stream") { FileDownloadName = Path.GetFileName(filePath) };
        }

        [HttpPost("cache")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload() {
            // receive file from phone via cache
            //Logger.ConsoleRouterLog("Post", "/api/file/upload", DateTime.Now);

            var f = await Request.ReadFormAsync();
            var files = new List<IFormFile>(f.Files);
            long size = files.Sum(f => f.Length);

            foreach (var file in files) {
                //set file path
                string filePath;
                Console.WriteLine($"Now processing {file.FileName}");
                if (file.FileName != null) {
                    //filePath = "./SAVE/" + file.FileName;
                    filePath = config.StorePath + file.FileName;
                } else {
                    //filePath = "./SAVE/TEMP.unknow";
                    filePath = config.StorePath + "TEMP.unknow";
                }
                //set buffer_size
                int buffer_size = Utils.GetBufferSize(size);
                //Console.WriteLine($"buffer_size:{buffer_size}");

                if (file.Length > 0) {
                    using var stream = System.IO.File.Create(filePath);
                    using (var bar = new MProgressBar(file.FileName)) {
                        //add progress Bar

                        byte[] buffer = new byte[buffer_size];
                        int times = (int)(size / buffer_size) + 1, cnt = 0;
                        var formFileStream = file.OpenReadStream();

                        //read formfile and write into target
                        int readCnt = formFileStream.Read(buffer, 0, buffer_size);
                        while (readCnt == buffer_size) {
                            stream.Write(buffer, 0, buffer_size);

                            readCnt = formFileStream.Read(buffer, 0, buffer_size);
                            bar.Tick(100 * cnt++ / times);
                        }
                        stream.Write(buffer, 0, readCnt);
                        bar.Tick(100);
                    }

                    Console.WriteLine($"Save file  in : {filePath}");
                }
            }
            return Ok(new { count = files.Count, size, state = "success" });
        }

        [HttpPost("stream")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadingStream() {
            Logger.ConsoleRouterLog("stream", "/api/file/stream", DateTime.Now);
            // receive file from phone via stream
            //获取boundary
            var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;

            //得到reader
            var reader = new MultipartReader(boundary, Request.Body);
            var fileLen = Request.ContentLength;
            var section = await reader.ReadNextSectionAsync();
            //test
            string _targetFilePath = config.StorePath;
            //读取section
            while (section != null) {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
                //section.ContentDisposition.

                if (hasContentDispositionHeader) {

                    var trustedFileNameForFileStorage = contentDisposition.FileName.Value;
                    await WriteFileAsync(section.Body, Path.Combine(_targetFilePath, trustedFileNameForFileStorage), (long)fileLen);
                }
                section = await reader.ReadNextSectionAsync();
            }

            return Created(nameof(FileController), new { state = "seccess" });
        }

        [HttpGet("upload")]
        public ContentResult UPLOAD() {
            //Logger.ConsoleRouterLog("Get", "/api/file/upload", DateTime.Now);
            return base.Content(UploadPage.html, "text/html");
        }


        public static async Task<long> WriteFileAsync(Stream stream, string path, long fileLen) {

            int num = 0;
             int FILE_WRITE_SIZE = Utils.GetBufferSize(fileLen);//写出缓冲区大小
            long writeCount = 0;
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write, FILE_WRITE_SIZE, true)) {
                using (var bar = new MProgressBar(path)) {
                    bar.Tick(0);
                    byte[] byteArr = new byte[FILE_WRITE_SIZE];
                    int readCount = 0;
                    while ((readCount = await stream.ReadAsync(byteArr, 0, byteArr.Length)) > 0) {
                        await fileStream.WriteAsync(byteArr, 0, readCount);
                        writeCount += readCount;
                        //Console.WriteLine(100 * writeCount / fileLen);
                        bar.Tick((int)(100 * writeCount / fileLen));
                    }
                    bar.Tick(100);
                }
            }
            Console.WriteLine($"sum and readCount::{num},{writeCount}");
            return writeCount;
        }
    }
}