namespace QRFT.Model {

    public class UploadPage {

        public static readonly string html = "\uFEFF<!DOCTYPE html>\n" +
                 "<html lang=\"en\">\n" +
                 "\n" +
                 "<head>\n" +
                 "    <meta charset=\"UTF-8\">\n" +
                 "    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n" +
                 "    <title>QRFT</title>\n" +
                 "    <style>\n" +
                 "        body {\n" +
                 "            margin: 0;\n" +
                 "            padding: 0;\n" +
                 "            background: center url(https://i.loli.net/2020/10/04/HOJcRumzboxT9KC.png) no-repeat;\n" +
                 "            background-size: cover;\n" +
                 "            vertical-align: middle;\n" +
                 "        }\n" +
                 "\n" +
                 "        @media screen and (max-width:900px) {\n" +
                 "            #warpper {\n" +
                 "                width: 80%;\n" +
                 "                margin: 0 auto;\n" +
                 "                text-align: center;\n" +
                 "                -webkit-box-shadow: 0px 3px 3px #c8c8c8;\n" +
                 "                height: 640px;\n" +
                 "                border-radius: 10px;\n" +
                 "                background-color: rgb(252, 248, 232);\n" +
                 "                -moz-box-shadow: 0px 3px 3px #c8c8c8;\n" +
                 "                opacity: 0.8;\n" +
                 "                box-shadow: 0px 3px 3px #c8c8c8;\n" +
                 "            }\n" +
                 "        }\n" +
                 "\n" +
                 "        @media screen and (min-width:901px) {\n" +
                 "            #warpper {\n" +
                 "                width: 40%;\n" +
                 "                margin: 0 auto;\n" +
                 "                text-align: center;\n" +
                 "                -webkit-box-shadow: 0px 3px 3px #c8c8c8;\n" +
                 "                height: 640px;\n" +
                 "                border-radius: 10px;\n" +
                 "                background-color: rgb(252, 248, 232);\n" +
                 "                -moz-box-shadow: 0px 3px 3px #c8c8c8;\n" +
                 "                opacity: 0.8;\n" +
                 "                box-shadow: 0px 3px 3px #c8c8c8;\n" +
                 "            }\n" +
                 "        }\n" +
                 "\n" +
                 "        #upload {\n" +
                 "            margin: 0 auto;\n" +
                 "            text-align: center;\n" +
                 "        }\n" +
                 "    </style>\n" +
                 "</head>\n" +
                 "\n" +
                 "<body>\n" +
                 "    <div id=\"warpper\">\n" +
                 "        <div id='upload'>\n" +
                 "            <H2>QRFT</H2>\n" +
                 "            <form id='upload_file'>\n" +
                 "                <input id='file_input' placeholder=\"select your file\" type=\"file\" ,name=\"files\" multiple>\n" +
                 "                <button>upload</button>\n" +
                 "            </form>\n" +
                 "        </div>\n" +
                 "    </div>\n" +
                 "    <script>\n" +
                 "        const upload_form = document.getElementById('upload_file')\n" +
                 "        const file_input = document.getElementById(\"file_input\")\n" +
                 "        upload_form.addEventListener('submit', (e) => {\n" +
                 "            e.preventDefault();\n" +
                 "            const formData = new FormData(upload_form);\n" +
                 "            const files = e.target[0].files\n" +
                 "            for (let file of files) {\n" +
                 "                console.log(e);\n" +
                 "                const currentDate = new Date().toJSON().slice(0, 19).replace(/[-T]/g, '_')\n" +
                 "\n" +
                 "                const filename = file.name || (\"qrft-\" + currentDate);\n" +
                 "                console.log(\"filename::\", file);\n" +
                 "\n" +
                 "                var data = new FormData();\n" +
                 "                data.append(\"\", file, filename);\n" +
                 "                var xhr = new XMLHttpRequest();\n" +
                 "                xhr.withCredentials = true;\n" +
                 "                xhr.addEventListener(\"readystatechange\", function () {\n" +
                 "                    if (this.readyState === 4) {\n" +
                 "                        console.log(this.responseText);\n" +
                 "                    }\n" +
                 "                });\n" +
                 "                xhr.open(\"POST\", \"/api/file/upload\");\n" +
                 "                xhr.send(data);\n" +
                 "            }\n" +
                 "        })\n" +
                 "\n" +
                 "            //     const blob = new Blob([file],{\n" +
                 "            //         type: \"text/plain\"\n" +
                 "            //     })\n" +
                 "\n" +
                 "            //     formData.append(\"textFile\", blob, filename)\n" +
                 "            //     axios.post('/api/file/upload', formData, {\n" +
                 "            //         headers: {\n" +
                 "            //             'Content-Type': 'multipart/form-data'\n" +
                 "            //         }\n" +
                 "            //     }).then(res => {\n" +
                 "            //         console.log(res.data);\n" +
                 "            //     })\n" +
                 "            // })\n" +
                 "    </script>\n" +
                 "</body>\n" +
                 "\n" +
                 "</html>";
    }
}