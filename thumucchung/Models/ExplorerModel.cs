using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace thumucchung.Models
{

    public class DirModel
    {
        public string DirName { get; set; }
        public DateTime DirAccessed { get; set; }


        public string ParentDirName { get; set; }
        public string FullDirName { get; set; }
        public bool hasChildren { get; set; }
        public virtual List<DirModel> children { get; set; }

        public string text { get; set; }//for treeview

        public string FlagUrl { get; set; }

    }

    public class FileModel
    {
        public string FileName { get; set; }
        public string FullFileName { get; set; }
        public string FileSizeText { get; set; }
        public DateTime FileAccessed { get; set; }
        public string FileExt { get; set; }

        public string DirName { get; set; }

        public string ParentFileDir { get; set; }
    }

    public class ExplorerModel
    {
        public List<DirModel> dirModelList;
        public List<FileModel> fileModelList;

        public ExplorerModel()
        {
        }

        public ExplorerModel(List<DirModel> _dirModelList, List<FileModel> _fileModelList)
        {
            dirModelList = _dirModelList;
            fileModelList = _fileModelList;
        }
    }
}
