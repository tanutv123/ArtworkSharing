﻿using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Presentation.Extensions;
using Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Pages.Home
{


    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IArtworkRepository _artworkRepository;

        public IndexModel(IArtworkRepository artworkRepository)
        {
            _artworkRepository = artworkRepository;
        }

        public IEnumerable<Artwork> Artworks { get; set; } 
        [BindProperty]
        public AddLikeDTO AddLikeDTO { get; set; }
        [BindProperty]
        public AddFollowDTO AddFollowDTO { get; set; }

        //Paging--------------------------------------------------------------------
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        //----------------------------------------------------------------------------
        public async Task OnGetAsync()
        {
            Artworks = await _artworkRepository.GetPaginatedResult(CurrentPage, PageSize);
            Count = await _artworkRepository.GetCount();            
            /*Artworks = await _artworkRepository.GetArtworks();*/

        }

        public async Task<IActionResult> OnPostIndex(string title)
        {
            if (System.String.IsNullOrEmpty(title))
            {
                Artworks = await _artworkRepository.GetArtworks();
                return Page();

            }
            else
            {
                Artworks = await _artworkRepository.GetArtworkByTitle(title);
                return Page();
            }
        }
    }
}
