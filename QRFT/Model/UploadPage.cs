namespace QRFT.Model {

    public class UploadPage {

        public static readonly string html = "<!DOCTYPE html>\n" +
                "<html lang=\"en\">\n" +
                "\n" +
                "<head>\n" +
                "    <meta charset=\"UTF-8\">\n" +
                "    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n" +
                "    <title>QRFT</title>\n" +
                "    <style>\n" +
                "        body {\n" +
                "            margin: 0;\n" +
                "            font-size: 2em;\n" +
                "            padding: 0;\n" +
                "            background: center url(https://i.loli.net/2020/10/04/HOJcRumzboxT9KC.png) no-repeat;\n" +
                "            background-size: cover;\n" +
                "            vertical-align: middle;\n" +
                "        }\n" +
                "        #warpper{\n" +
                "            font-size: 1em;\n" +
                "            padding:0em 0.5em;\n" +
                "        }\n" +
                "        @media screen and (max-width:900px)and (min-width: 500px){\n" +
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
                "\n" +
                "                box-shadow: 0px 3px 3px #c8c8c8;\n" +
                "            }\n" +
                "        }\n" +
                "\n" +
                "        @media screen and (min-width:901px) {\n" +
                "            #warpper {\n" +
                "                padding:0em 0.5em;\n" +
                "                width: 45%;\n" +
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
                "\n" +
                "            margin: 0 auto;\n" +
                "            text-align: center;\n" +
                "        }\n" +
                "\n" +
                "        button {\n" +
                "            font-size: 0.5em;\n" +
                "            padding: 0em 1em;\n" +
                "        }\n" +
                "\n" +
                "        button:active {\n" +
                "            background-color: #c6f5c8;\n" +
                "            transform: translateY(4px);\n" +
                "          \n" +
                "        }\n" +
                "\n" +
                "        \n" +
                "\n" +
                "        button:hover {\n" +
                "            box-shadow: 0 12px 16px 0 rgba(0, 0, 0, 0.24), 0 17px 50px 0 rgba(0, 0, 0, 0.19);\n" +
                "        }\n" +
                "\n" +
                "        input{\n" +
                "            \n" +
                "            font-size: 0.5em;\n" +
                "            display: inline;\n" +
                "        }\n" +
                "\n" +
                "        @media screen and (max-width: 501px) {\n" +
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
                "\n" +
                "                box-shadow: 0px 3px 3px #c8c8c8;\n" +
                "            }\n" +
                "            #upload_btn{\n" +
                "                display: block;\n" +
                "                margin:3em auto;\n" +
                "                text-align: center;\n" +
                "                \n" +
                "            }\n" +
                "            #file_input{\n" +
                "                margin: 0em auto;\n" +
                "                display: block;\n" +
                "                text-align: center;\n" +
                "            }\n" +
                "            #upload_file{\n" +
                "                margin: 0 auto;\n" +
                "            }\n" +
                "        }\n" +
                "    </style>\n" +
                "</head>\n" +
                "\n" +
                "<body>\n" +
                "    <div id=\"warpper\">\n" +
                "        <div id='upload'>\n" +
                "            <H2>QRFT</H2>\n" +
                "            <form id='upload_file'>\n" +
                "                <input id='file_input'  placeholder=\"select your file\" type=\"file\" ,name=\"files\" multiple>\n" +
                "                <button id='upload_btn'>上传</button>\n" +
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
                "                console.log(data);" +
                "                var xhr = new XMLHttpRequest();\n" +
                "                xhr.withCredentials = true;\n" +
                "                xhr.addEventListener(\"readystatechange\", function () {\n" +
                "                    if (this.readyState === 4) {\n" +
                "                        console.log(this.responseText);\n" +
                "                    }\n" +
                "                });\n" +
                $"                xhr.open(\"POST\", \"/api/file/stream\");\n" +
                "                xhr.send(data);\n" +
                "            }\n" +
                "        })\n" +
                "\n" +
                "        //     const blob = new Blob([file],{\n" +
                "        //         type: \"text/plain\"\n" +
                "        //     })\n" +
                "\n" +
                "        //     formData.append(\"textFile\", blob, filename)\n" +
                "        //     axios.post('/api/file/upload', formData, {\n" +
                "        //         headers: {\n" +
                "        //             'Content-Type': 'multipart/form-data'\n" +
                "        //         }\n" +
                "        //     }).then(res => {\n" +
                "        //         console.log(res.data);\n" +
                "        //     })\n" +
                "        // })\n" +
                "    </script>\n" +
                "</body>\n" +
                "\n" +
                "</html>";

    }
}