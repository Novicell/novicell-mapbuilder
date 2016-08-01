# Novicell MapBuilder

MapBuilder is an umbraco package for easily generating Google Maps based on umbraco nodes. Perfect for generating a map with physical stores.


### Table of content
* [Quick start](#quick-start)
* [Requirements](#requirements)
* [License](#license)

## Quick start

To go `https://our.umbraco.org/projects/website-utilities/mapbuilder/` and download the package. Then go to your backoffice > Developer > Packages, and install it as usual.

After the installation completes. Go to the new custom section, and setup a datasource. After you've created a datasource, create a new map, and set the datasource to the newly created one.

Go to the map and copy the alias. We need this for the next part.

When you're all set in the custom section, go to the view where you wish for the map to be showed. Use @Html.RenderMap(MAPALIAS), and that's it.

## Requirements
Umbraco 7.4+ is required

## License
MIT License - `http://opensource.org/licenses/MIT`
