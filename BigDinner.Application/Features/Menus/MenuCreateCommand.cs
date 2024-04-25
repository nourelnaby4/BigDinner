using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Application.Features.Menus
{
    public record MenuCreateCommand(
        string Name,
        string Description,
        List<MenuSection> Sections);

    public record MenuSection(
        string Name,
        string Description,
        List<MenuItem> Items);

    public record MenuItem(
        string Name,
        string Description);

}
