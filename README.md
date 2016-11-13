# PersianHelpers
Some helper classes to work with Persian culture easily in .net
## Installation
`PM> Install-Package PersianHelpers`
## Usage
Just instantiate `PersianCulture` and assign it to `CurrentCulture`:
``` cs
CultureInfo.DefaultThreadCurrentCulture = new PersianCulture();
CultureInfo.DefaultThreadCurrentUICulture = new PersianCulture();
```
