using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFDigitalSignatureBySelfSignedCertificate
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
                前言：最近有个需求是需要对文档进行数字签名；
                描述：本示例基于Spire.Pdf组件对PDF进行数字签名，演示了
                    签名证书使用项目
            CreateSelfSignedCertificateByBouncyCastle(https://github.com/daileass/CreateSelfSignedCertificateByBouncyCastle.git)
                    生成的自签名证书pfx，解决了数字签名后文档头部有警告
                
            */

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
        }

        /// <summary>
        /// 将文件转换为byte数组
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns>转换后的byte数组</returns>
        public static byte[] File2Bytes(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return new byte[0];
            }

            FileInfo fi = new FileInfo(path);
            byte[] buff = new byte[fi.Length];

            FileStream fs = fi.OpenRead();
            fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            return buff;
        }
    }
}
