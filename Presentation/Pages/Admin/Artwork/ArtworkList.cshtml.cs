using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Admin.Artwork;

[Authorize(Policy = "RequireAdminRole")]
public class ArtworkList : PageModel
{
    private readonly IArtworkRepository _artworkRepository;
    public ArtworkList(IArtworkRepository artworkRepository)
    {
        _artworkRepository = artworkRepository;
    }
    
    [BindProperty]
    public IList<BusinessObject.Entities.Artwork> Artworks { get; set; }
    public async Task OnGet()
    {
        Artworks = await _artworkRepository.GetArtworkAdmin();
    }
}