# Novicell MapBuilder

MapBuilder is an Umbraco package for easily generating Google Maps based on Umbraco nodes. Perfect for generating a map with physical stores.


### Table of content
* [Quick start](#quick-start)
* [Public methods](#public-methods)
* [Requirements](#requirements)
* [Found a bug?](#found-a-bug)
* [Improvements?](#improvements)
* [Login for development](#login)
* [License](#license)

## Quick start
Go to https://our.umbraco.org/projects/website-utilities/mapbuilder/ and download the package.

Then go to your backoffice > Developer > Packages > Install local package, and install it as usual. Accept when you're told that the package contains binary files.

After the installation completes, go to the new custom section and setup a datasource (if you don't see the new section, go to the Users section and enable it for yourself and any others that need access). After you've created a datasource, create a new map, and set its datasource to the newly created one.

Go to the map and copy the alias. We need this for the next part.

When you're all set in the custom section, go to the view where you wish for the map to be showed. Use `@Html.RenderMap(MAPALIAS)`, and that's it.

## Public methods
The methods available for use is:

```C#
public static MvcHtmlString RenderMap(this HtmlHelper htmlHelper, string mapAlias)
```

```C#
public static MvcHtmlString RenderMap(this HtmlHelper htmlHelper, string mapAlias, List<int> nodeIds)
```

```C#
public static MvcHtmlString RenderMap(this HtmlHelper htmlHelper, string mapAlias, List<int> nodeIds, string titleProperty, string coordsProperty)
```

Each of the methods has inline summaries of what is expected from them.

## Requirements
Umbraco 7.4+

It MAY work in earlier versions, but this has not been tested.

## Found a bug?
You're more than welcome to fork the project and fix any bug you may find. Submit a pull request, and we will review it as quickly as possible, and merge it when approved.

## Improvement?
If you have an improvement for the project, please fork the project and create a pull request with the change. We will review it as quickly as possible, and merge it when approved.

## Login for development
To log into Umbraco when developing for this repository, please use the following credentials:

	Username: admin
	Password: admin1234

The setup uses Microsoft SQL CE (Compact Edition) to avoid having to setup a new database and cloning in a .bak file.

## License
MIT License - http://opensource.org/licenses/MIT

