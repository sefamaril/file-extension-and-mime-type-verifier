namespace FileExtensionandMimeTypeVerifier.File
{
    public class CheckFile
    {
        //We want to control mime types(as byte) and extensions 
        private static readonly byte[] DOC = { 208, 207, 17, 224, 161, 177, 26, 225 };
        private static readonly byte[] ZIP_DOCX = { 80, 75, 3, 4 };
        private static readonly byte[] MS_MPP = { 85, 69, 78, 67, 79, 68, 73, 78, 71, 32, 45, 45, 45 };
        private static readonly byte[] TXT_CSV = { 116, 101, 120, 116, 47, 99, 115, 118 };
        private static readonly byte[] CSV = { 116, 101, 120, 116, 47, 99, 115, 118 };
        private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
        private static readonly byte[] JPG = { 255, 216, 255 };
        private static readonly byte[] PNG = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 };
        private static string[] extensions = { "pdf", "doc", "docx", "docm", "dotx", "dotm", "docb", "dot", "rtf", "wps", "wpt", "wpd", "odt", "fodt", "ott", "uot",
                                               "xls", "xlsx", "xlsm", "xlsb", "xltx", "xltm", "xlam", "xla", "xlm", "xlw", "ods", "fods", "ots", "uos", "csv",
                                               "ppt", "pptx", "pptm", "potx", "potm", "ppam", "ppa", "pps", "ppsm", "odp",
                                               "mpp", "mpt", "mpx", "txt", "png", "jpg", "jpeg", "jpe", "jfif", "exif", "pjpeg" };

        public static string CheckMimeType(byte[] file, string fileName)
        {
            string mime = "application/octet-stream"; //Default unknown mime type

            if (string.IsNullOrWhiteSpace(fileName))
                return mime;
            string extension = Path.GetExtension(fileName) == null ? string.Empty : Path.GetExtension(fileName).ToUpper().TrimStart('.');

            switch (true)
            {
                case var _ when file.Take(8).SequenceEqual(DOC):
                    mime = "application/msword";
                    break;
                case var _ when file.Take(4).SequenceEqual(ZIP_DOCX):
                    mime = extension == ".DOCX" ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document" : "application/x-zip-compressed";
                    break;
                case var _ when file.Take(13).SequenceEqual(MS_MPP):
                    mime = "application/x-msproject";
                    break;
                case var _ when file.Take(8).SequenceEqual(TXT_CSV):
                    mime = "text/plain";
                    break;
                case var _ when file.Take(7).SequenceEqual(PDF):
                    mime = "application/pdf";
                    break;
                case var _ when file.Take(3).SequenceEqual(JPG):
                    mime = "image/jpeg";
                    break;
                case var _ when file.Take(16).SequenceEqual(PNG):
                    mime = "image/png";
                    break;
            }

            return mime;
        }
        public static bool CheckExtension(string extension)
        {
            return extensions.Contains(extension);
        }
    }
}
