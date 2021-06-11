using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.IO;
using TuesPechkin;
using System.Drawing.Printing;
using System.Web.Hosting;

/// <summary>
/// Summary description for Pdfhelper
/// </summary>
public class Pdfhelper {


        public static string BuildInvoicePdf(string invoice) {
            string host = HttpContext.Current.Request.Url.Authority;
            string page = "http://gpos.gposnz.com/admin/invoice.aspx?id=" + invoice + "&pdf=wkhtmltopdf";
            string pdfpath = "~/export/pdf/invoice/inv#" + invoice + ".pdf"; //生成的PDF文件路径 
            if (!IsExistInvoicePdf(invoice)) {
                BuildPdf(page, pdfpath);
            }
            return HttpContext.Current.Server.MapPath(pdfpath);
        }

        public static bool IsExistInvoicePdf(string invoice) {
            string path = HttpContext.Current.Server.MapPath("~/export/pdf/invoice/inv#" + invoice + ".pdf");
            if (File.Exists(path)) {
                return true;
            } else {
                return false;
            }
        }
        public static string BuildPdf(string url, string path) {
            var document = new HtmlToPdfDocument {
                GlobalSettings = {
                        ProduceOutline = true,
                        PaperSize = PaperKind.A4, // Implicit conversion to PechkinPaperSize
                    },
                    Objects = {
                        new ObjectSettings {
                            PageUrl = url
                        }
                    }
            };

            // Keep the converter somewhere static, or as a singleton instance!
            // Do NOT run the above code more than once in the application lifecycle!

            byte[] result = ConvertFactory.GetConverter().Convert(document);
          
            string pdfpath = HttpContext.Current.Server.MapPath(path); //生成的PDF文件路径  
            try{
                  using ( FileStream pFileStream = new FileStream(pdfpath, FileMode.OpenOrCreate)){
                     pFileStream.Write(result, 0, result.Length);
                     return pdfpath;
                }
            }
            catch(Exception e){
                return null;
            }
          
           
       
            // pFileStream.Close();
            
        }

        public class ConvertFactory {
            /// <summary>
            /// The static readonly locker
            /// </summary>
            private static readonly object Locker = new object();

            /// <summary>
            /// Pdf converter
            /// </summary>
            private static IConverter converter;

            /// <summary>
            /// Singleton converter, for multi-threaded application
            /// </summary>
            /// <returns>Pdf converter</returns>
            public static IConverter GetConverter() {
                lock(Locker) {
                    if (converter != null) {
                        return converter;
                    }
                    var tempFolderDeployment = new TempFolderDeployment();
                    var winAnyCpuEmbeddedDeployment = new WinAnyCPUEmbeddedDeployment(tempFolderDeployment);
                    IToolset toolSet;
                    if (HostingEnvironment.IsHosted) {
                        toolSet = new RemotingToolset < PdfToolset > (winAnyCpuEmbeddedDeployment);
                    } else {
                        toolSet = new PdfToolset(winAnyCpuEmbeddedDeployment);
                    }

                    converter = new ThreadSafeConverter(toolSet);
                }

                return converter;
            }
        }
    }
    // //convert must be static
    // public static class Convert{
    //     public static   IConverter converter =
    //       new ThreadSafeConverter(
    //           new RemotingToolset<PdfToolset>(
    //               new WinAnyCPUEmbeddedDeployment(
    //                   new TempFolderDeployment())));
    // }