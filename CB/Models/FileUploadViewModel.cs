using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CB.Models
{
    public class FileUploadViewModel
    {

        public List<FileOnDatabaseModel> FilesOnDatabase { get; set; }

        internal object Where(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
