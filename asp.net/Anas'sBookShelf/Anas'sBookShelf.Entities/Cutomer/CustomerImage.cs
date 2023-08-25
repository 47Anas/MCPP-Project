using Anas_sBookShelf.Entities.Uploader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anas_sBookShelf.Entities.Cutomer
{
    public class CustomerImage : UploaderImage
    {
        public int CustomerId { get; set; }

    }
}
