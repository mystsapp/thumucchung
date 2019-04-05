namespace thumucchung.Models
{
    public static class TrUtility
    {

        public static string GetIconFile(string fileExt)
        {
            string s = "";

            switch (fileExt.ToLower())
            {
                case "pdf":
                    s = "pdf_icon.png";
                    break;
                case "xls":
                    s = "excel_icon.png";
                    break;
                case "xlsx":
                    s = "excel_icon.png";
                    break;
                case "doc":
                    s = "word_icon.png";
                    break;
                case "docx":
                    s = "word_icon.png";
                    break;
                case "ppt":
                    s = "powerpoint_icon.png";
                    break;
                case "pptx":
                    s = "powerpoint_icon.png";
                    break;
                case "epub":
                    s = "epub_icon.png";
                    break;
               
                case "txt":
                    s = "txt_icon.png";
                    break;
                case "lnk":
                    s = "txt_icon.png";
                    break;
                case "jpeg":
                    s = "jpeg-icon.png";
                    break;
                case "jpg":
                    s = "jpeg-icon.png";
                    break;
                default:
                    s = "";
                    break;
            }

            return s;
        }
    }
}