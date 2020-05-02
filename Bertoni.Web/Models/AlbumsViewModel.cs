using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bertoni.Web.Models {
    public class AlbumsViewModel {
        public int AlbumId { get; set;}
        public List<SelectListItem> Albums { get; set; }
    }
}