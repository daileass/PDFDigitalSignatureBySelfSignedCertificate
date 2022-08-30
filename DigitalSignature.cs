
using Spire.Pdf;
using Spire.Pdf.Annotations;
using Spire.Pdf.Annotations.Appearance;
using Spire.Pdf.Graphics;
using Spire.Pdf.Interactive.DigitalSignatures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PDFDigitalSignatureBySelfSignedCertificate
{
    public class DigitalSignature
    {
        private const string WatermarkCoverBase64 = "/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAMCAgMCAgMDAwMEAwMEBQgFBQQEBQoHBwYIDAoMDAsKCwsNDhIQDQ4RDgsLEBYQERMUFRUVDA8XGBYUGBIUFRT/2wBDAQMEBAUEBQkFBQkUDQsNFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBT/wAARCABHAycDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD9U6KKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooA//Z";

        public DigitalSignature(byte[] waitSignFile, byte[] imageSign, byte[] pfx, string pfxPwd)
        {
            this.WaitSignFile = waitSignFile;
            this.ImageSign = imageSign;
            this.Pfx = pfx;
            this.PfxPwd = pfxPwd;
        }

        public DigitalSignature(byte[] waitSignFile, string charactersSign, float signRightLeftWidth, float signBottomUpHeight, byte[] pfx, string pfxPwd)
        {
            this.WaitSignFile = waitSignFile;
            this.CharactersSign = charactersSign;
            this.SignRightLeftWidth = signRightLeftWidth;
            this.SignBottomUpHeight = signBottomUpHeight;
            this.Pfx = pfx;
            this.PfxPwd = pfxPwd;
        }

        public DigitalSignature(byte[] waitSignFile, byte[] imageSign, string charactersSign, byte[] pfx, string pfxPwd)
        {
            this.WaitSignFile = waitSignFile;
            this.ImageSign = imageSign;
            this.CharactersSign = charactersSign;
            this.Pfx = pfx;
            this.PfxPwd = pfxPwd;
        }

        /// <summary>
        /// Gets or sets 待签名文件.
        /// </summary>
        public byte[] WaitSignFile { get; set; }

        /// <summary>
        /// Gets or sets 图签名.
        /// </summary>
        public byte[] ImageSign { get; set; }

        /// <summary>
        /// Gets or sets 文字签名.
        /// </summary>
        public string CharactersSign { get; set; }

        /// <summary>
        /// Gets or sets 签名右向左的宽度.
        /// </summary>
        public float? SignRightLeftWidth { get; set; }

        /// <summary>
        /// Gets or sets 签名顶向上高度.
        /// </summary>
        public float? SignBottomUpHeight { get; set; }

        /// <summary>
        /// Gets or sets 签名索引页面（不指定默认所有页进行签名）.
        /// </summary>
        public int? SignIndexPages { get; set; }

        /// <summary>
        /// Gets or sets Pfx证书.
        /// </summary>
        public byte[] Pfx { get; set; }

        /// <summary>
        /// Gets or sets Pfx证书密码.
        /// </summary>
        public string PfxPwd { get; set; }

        public Stream Signature()
        {
            ///加载PDF文档
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromBytes(this.WaitSignFile);

            if (pdf?.Pages?.Count <= 0)
            {
                throw new Exception("文件有误");
            }

            X509Certificate2 x509 = new X509Certificate2(this.Pfx, this.PfxPwd);
            PdfOrdinarySignatureMaker signatureMaker = new PdfOrdinarySignatureMaker(pdf, x509);

            var appearance = new PdfCustomSignatureAppearance(this.CharactersSign, this.ImageSign, this.SignRightLeftWidth, this.SignBottomUpHeight);
            IPdfSignatureAppearance signatureAppearance = appearance;

            // 绘画白底图片
            PdfRubberStampAnnotation logoStamp = new PdfRubberStampAnnotation(new RectangleF(new PointF(0, 0), new SizeF(350, 22)));
            PdfAppearance logoApprearance = new PdfAppearance(logoStamp);
            //var logoPath = AppDomain.CurrentDomain.BaseDirectory + "\\white.jpg";
            byte[] byt = Convert.FromBase64String(WatermarkCoverBase64);
            Stream streamByLogo = new MemoryStream(byt);
            PdfImage image = PdfImage.FromStream(streamByLogo);
            PdfTemplate template = new PdfTemplate(350, 22);
            template.Graphics.DrawImage(image, 0, 0);
            logoApprearance.Normal = template;
            logoStamp.Appearance = logoApprearance;

            if (this.SignIndexPages.HasValue)
            {
                if (this.SignIndexPages.Value < 0 || this.SignIndexPages.Value > pdf?.Pages?.Count)
                {
                    throw new Exception("签名索引页有误");
                }

                var page = pdf.Pages[this.SignIndexPages.Value];

                // 添加白底图片覆盖页面顶部印记
                page.AnnotationsWidget.Add(logoStamp);

                // 在页面中的指定位置添加可视化签名
                signatureMaker.MakeSignature("signName_", page, page.ActualSize.Width - appearance.SignRightLeftWidth, page.ActualSize.Height - appearance.SignBottomUpHeight, appearance.SignRightLeftWidth, appearance.SignBottomUpHeight, signatureAppearance);
            }
            else
            {
                foreach (PdfPageBase page in pdf.Pages)
                {
                    // 添加白底图片覆盖页面顶部印记
                    page.AnnotationsWidget.Add(logoStamp);

                    // 在页面中的指定位置添加可视化签名
                    signatureMaker.MakeSignature("signName_", page, page.ActualSize.Width - appearance.SignRightLeftWidth, page.ActualSize.Height - appearance.SignBottomUpHeight, appearance.SignRightLeftWidth, appearance.SignBottomUpHeight, signatureAppearance);
                }
            }

            MemoryStream stream = new MemoryStream();
            pdf.SaveToStream(stream, FileFormat.PDF);
            pdf.Close();
            return stream;
        }

        /// <summary>
        /// 使用第三方插件 =》 去除  Evaluation Warning : The document was created with Spire.PDF for .NET.
        /// </summary>
        /// <param name="sourcePdfs">原文件地址</param>
        //private static MemoryStream ClearPdfFilesFirstPage(MemoryStream sourcePdf)
        //{
        //    iTextSharp.text.pdf.PdfReader reader = null;
        //    iTextSharp.text.Document document = new iTextSharp.text.Document();
        //    iTextSharp.text.pdf.PdfImportedPage page = null;
        //    iTextSharp.text.pdf.PdfCopy pdfCpy = null;
        //    int n = 0;
        //    reader = new iTextSharp.text.pdf.PdfReader(sourcePdf);
        //    reader.ConsolidateNamedDestinations();
        //    n = reader.NumberOfPages;
        //    document = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(1));
        //    MemoryStream memoryStream = new MemoryStream();
        //    pdfCpy = new iTextSharp.text.pdf.PdfCopy(document, memoryStream);
        //    document.Open();
        //    for (int j = 2; j <= n; j++)
        //    {
        //        page = pdfCpy.GetImportedPage(reader, j);
        //        pdfCpy.AddPage(page);

        //    }
        //    reader.Close();
        //    document.Close();
        //    return memoryStream;
        //}
    }


    public class PdfCustomSignatureAppearance : IPdfSignatureAppearance
    {
        public PdfCustomSignatureAppearance(string charactersSign, byte[] sign, float? signRightLeftWidth, float? signBottomUpHeight)
        {
            this.CharactersSign = charactersSign;

            if (sign != null && sign.Length > 0)
            {
                this.Sign = sign;
                MemoryStream ms = new MemoryStream(sign);
                var image = System.Drawing.Image.FromStream(ms);
                if (!signRightLeftWidth.HasValue)
                {
                    signRightLeftWidth = image.Width;
                }

                if (!signBottomUpHeight.HasValue)
                {
                    signBottomUpHeight = image.Height;
                }
            }

            this.SignRightLeftWidth = signRightLeftWidth.Value;
            this.SignBottomUpHeight = signBottomUpHeight.Value;
        }

        /// <summary>
        /// Gets or sets 签名.
        /// </summary>
        public byte[] Sign { get; set; }

        /// <summary>
        /// Gets or sets 签名右向左的宽度.
        /// </summary>
        public float SignRightLeftWidth { get; set; }

        /// <summary>
        /// Gets or sets 签名顶向上高度.
        /// </summary>
        public float SignBottomUpHeight { get; set; }

        /// <summary>
        /// Gets or sets 文字签名.
        /// </summary>
        public string CharactersSign { get; set; }

        public void Generate(PdfCanvas g)
        {
            if (!string.IsNullOrWhiteSpace(CharactersSign))
            {
                float fontSize = 15;
                var font = new System.Drawing.Font("Arial", fontSize);
                PdfTrueTypeFont fontByPdf = new PdfTrueTypeFont(font, true);
                g.DrawString(CharactersSign, fontByPdf, PdfBrushes.Black, new PointF(0, 0));
            }

            if (this.Sign != null && this.Sign.Length > 0)
            {
                Stream stream = new MemoryStream(this.Sign);
                g.DrawImage(Spire.Pdf.Graphics.PdfImage.FromStream(stream), new PointF(20, 20));
            }
        }
    }
}
