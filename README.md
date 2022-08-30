# PDFDigitalSignatureBySelfSignedCertificate
# PDF数字签名  自签名CA证书  Spire.Pdf

前言：最近有个需求是需要对文档进行数字签名；
描述：本示例基于Spire.Pdf组件对PDF进行数字签名，演示了签名证书使用项目
            CreateSelfSignedCertificateByBouncyCastle(https://github.com/daileass/CreateSelfSignedCertificateByBouncyCastle.git)
生成的自签名证书pfx，解决了数字签名后文档头部有警告

部分代码：
            var fileCert = System.Environment.CurrentDirectory + "\\Cert\\";
            var file = System.Environment.CurrentDirectory + "\\File\\";
            var filePath = file + "dome.pdf";
            var newFilePath = file + $"dome_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            var pfxFilePath = fileCert + "edd9386229324d969692dcabf97ac095dpps.fun.pfx";
            var pfxFilePwd = "ABCD123456";
            var signFilePath = file + "sign.png";

            // 数字签名
            var digitalSignature = new DigitalSignature(
                File2Bytes(filePath),
                File2Bytes(signFilePath),
                "Sign Here:",
                File2Bytes(pfxFilePath),
                pfxFilePwd
                );
            var stream = digitalSignature.Signature();

            // 保存签名后的文件
            using (var fileStream = File.Create(newFilePath))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }

            Console.WriteLine("OK");
            Console.ReadLine();
