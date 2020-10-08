﻿using Microsoft.AspNetCore.Http;
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
        private static Config config = Config.getInstance();
        private string filePath;
        private string hash;

        public FileController() {
            filePath = config.FilePath;
            hash = config.Hash;
        }

        [HttpGet("{hash}")]
        public IActionResult Get(string hash) {
            Logger.consoleRouterLog("Post", "/api/file/upload", DateTime.Now);
            // send file from computer
            if (!hash.Equals(this.hash)) {
                return NotFound();
            }

            var stream = System.IO.File.OpenRead(filePath);

            if (stream == null) {
                return NotFound();
            }
            return new FileStreamResult(stream, "application/octet-stream");
        }

        [HttpPost("upload")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload() {
            // receive file from phone via cache
            Logger.consoleRouterLog("Post", "/api/file/upload", DateTime.Now);

            var f = await Request.ReadFormAsync();
            var files = new List<IFormFile>(f.Files);
            long size = files.Sum(f => f.Length);

            foreach (var file in files) {
                //set file path
                string filePath;
                if (file.FileName != null) {
                    //filePath = "./SAVE/" + file.FileName;
                    filePath = "./" + file.FileName;
                } else {
                    //filePath = "./SAVE/TEMP.unknow";
                    filePath = "TEMP.unknow";
                }
                //set buffer_size
                int buffer_size = Utils.getBufferSize(size);

                if (file.Length > 0) {
                    using (var stream = System.IO.File.Create(filePath)) {
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
            }
            return Ok(new { count = files.Count, size, state = "success" });
        }

        [HttpPost("stream")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadingStream() {
            // receive file from phone via stream
            Console.WriteLine("visit stream");
            //获取boundary
            var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;
            Console.WriteLine(boundary);
            //得到reader
            var reader = new MultipartReader(boundary, Request.Body);
            //{ BodyLengthLimit = 2000 };//
            var section = await reader.ReadNextSectionAsync();
            //test
            string _targetFilePath = "./";
            //读取section
            while (section != null) {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
                if (hasContentDispositionHeader) {
                    var trustedFileNameForFileStorage = Path.GetRandomFileName();
                    await WriteFileAsync(section.Body, Path.Combine(_targetFilePath, trustedFileNameForFileStorage));
                }
                section = await reader.ReadNextSectionAsync();
            }
            Console.WriteLine("stream closed");
            return Created(nameof(FileController), null);
        }

        [HttpGet("upload")]
        public ContentResult UPLOAD() {
            Logger.consoleRouterLog("Get", "/api/file/upload", DateTime.Now);
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            return base.Content(UploadPage.html, "text/html");
        }

        public static async Task<int> WriteFileAsync(System.IO.Stream stream, string path) {
            const int FILE_WRITE_SIZE = 84975;//写出缓冲区大小
            int writeCount = 0;
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write, FILE_WRITE_SIZE, true)) {
                byte[] byteArr = new byte[FILE_WRITE_SIZE];
                int readCount = 0;
                while ((readCount = await stream.ReadAsync(byteArr, 0, byteArr.Length)) > 0) {
                    await fileStream.WriteAsync(byteArr, 0, readCount);
                    writeCount += readCount;
                }
            }
            return writeCount;
        }
    }
}