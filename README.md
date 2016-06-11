# WrLib.Utils.StringExpander
String Expander Library for .NET

## Example
  
```
  var dict = new Dictionary<string, string>();
  dict["name"] = "Smock";
  dict["phone"] = "180087763334";
  dict["info"] = "$(name):$(phone)";

  IStringExpander sx = new StringExpander(dict);

  Assert.AreEqual("Value of $(info) is 'Smock:180087763334'", sx.Expand("Value of $$(info) is '$(info)'"));
```
