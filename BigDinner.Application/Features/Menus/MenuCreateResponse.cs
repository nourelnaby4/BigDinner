using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Application.Features.Menus
{
    public record MenuCreateResponse(
         string Name,
         string Description,
         List<MenuSection> Sections);

    public record MenuSectionResponse(
        string Name,
        string Description,
        List<MenuItem> Items);

    public record MenuItemResponse(
        string Name,
        string Description);
}
