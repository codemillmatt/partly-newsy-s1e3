# Partly Cloudy Episode 3: If Forms Was a Turtle (Xamarin.Forms Shell)

Welcome back to Partly Cloudy! The show where you learn how to build a cloud-connected Xamarin mobile application. We start from nothing and don't quit until it's ready for the App Store!

In this episode we start to make our app look a little bit more like the finished one! We go from one that only displays headlines in a `ListView` to one that supports top and bottom tab, displays graphics in those tabs, and can perform navigation between different views of the app.

And all of this is done with a Xamarin.Forms feature called Shell.

## Episode Recap

We're building a clone of the Microsoft News app.

### Past Episodes 

In [episode 1](https://channel9.msdn.com/Shows/Partly-Cloudy/Hello-News-Intro-project-structure-and-HTTP-requests?WT.mc_id=mobile-0000-masoucou) we setup the project structure and started to call an Azure Function.

Then in [episode 2](https://channel9.msdn.com/Shows/Partly-Cloudy/Inform-Me-Bing-News-API?WT.mc_id=mobile-0000-masoucou) we made that Azure Function invoke the Bing News Search API, so we could view the news in our app.

### What Happened in This Episode

![Drawing of a turtle for episode 3](http://res.cloudinary.com/code-mill-technologies-inc/image/upload/c_scale,e_shadow:40,h_600/v1572449030/thumbnail_66403_n6kpbz.jpg)

In this episode we give our user interface some structure through [Xamarin.Forms Shell](https://docs.microsoft.com/xamarin/xamarin-forms/app-fundamentals/shell/?WT.mc_id=mobile-0000-masoucou).

#### Xamarin.Forms Shell

Shell reduces the amount of work that you have to do in order to creae a full-featured application. It provides a scaffolding to build tabs, flyout, navigation - including the ability to pass parameters via a URL based syntax, and more.

In other words - Shell is a prescriptive means of developing apps that saves you time by providing features for you so you don't have to development them from scratch over and over again.

#### The Play-by-Play

If you're building the app along with the show, download the code in this here repo!

You'll have to add a `local.settings.json` file to the Azure Functions project to make sure it can communicate to the Bing News Search API, but after that, you'll be good to go. [Here's a starter one](https://gist.github.com/codemillmatt/828ace7089a93fccd0ac4012e006d9a4) for you.

1. The first thing we did in the episode was to add a new `Pages` folder - and added a new `ContentPage` to it called `NewsCollectionPage.xaml`. Copied all the functionality over from the `MainPage.xaml` into it. This is setting us up for a more proper structured project long-term.
1. Then we introduced Shell by adding a page called `AppShellPage.xaml`.

This is the root page for Shell applications and everything is enclosed within `<Shell></Shell>` tags.

3. From there we added some [Shell Tabs](https://docs.microsoft.com/xamarin/xamarin-forms/app-fundamentals/shell/tabs?WT.mc_id=mobile-0000-masoucou) with a `<TabBar>` element. This will allow the `NewsCollectionPage` created above to sit within a tab at the bottom of the Shell.

So our Shell page starts to look like this:

```language-xaml
<?xml version="1.0" encoding="UTF-8"?>
<Shell
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:PartlyNewsy.Core"
    x:Class="PartlyNewsy.Core.AppShellPage"
    Shell.TabBarTitleColor="Red"
    Shell.TabBarBackgroundColor="White"
    Shell.TabBarDisabledColor="Gray">

    <TabBar>
        <Tab Title="News">
            <ShellContent Title="Ny News">
                <local:NewsCollectionPage />
            </ShellContent>
        </Tab>
    </TabBar>

</Shell>
```

One thing to note - if a `<TabBar>` element only has one `<Tab>` in it - it will _not_ display any tabs at all.

> If a `<TabBar>` only has one `<Tab> in it, it will not display any tabs at all.

4. By adding more `<Tab>` elements with pages within the `<ShellContent>` we were able to see more pages being added.

5. Next we wanted to add top tabs to the page. The way you do this is to add multiple `<ShellContent>` elements within a _single_ `<Tab>` element. 

This is telling Shell - hey - I want a bunch of sub pages in here - so Shell says - let's put them along the top!

```language-xaml
<?xml version="1.0" encoding="UTF-8"?>
<Shell
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:PartlyNewsy.Core"
    x:Class="PartlyNewsy.Core.AppShellPage"
    Shell.TabBarTitleColor="Red"
    Shell.TabBarBackgroundColor="White"
    Shell.TabBarDisabledColor="Gray">

    <TabBar>
        <Tab Title="News" Shell.NavBarIsVisible="False">
            <ShellContent Title="Ny News">
                <local:NewsCollectionPage />
            </ShellContent>
            <ShellContent Title="US News">
                <local:NewsCollectionPage />
            </ShellContent>
            <ShellContent Title="World News">
                <local:NewsCollectionPage />
            </ShellContent>
        </Tab>
        <Tab Title="Local News">
            <ShellContent>
                <local:NewsCollectionPage />
            </ShellContent>
        </Tab>
    </TabBar>

</Shell>
```

6. Then we spiced things up by adding graphics to the tabs along the bottom of the page.

We did this with glyphs from a special font ... kinda like Font Awesome. And the `FontImageSource` object.

You can use regular images too if you wanted to. But the cool thing about using a font is that you get a ton of images .. I mean glyphs all built into a single package, if you will.

7. Then finally we did some navigation, using Shell's [built-in navigation framework](https://docs.microsoft.com/xamarin/xamarin-forms/app-fundamentals/shell/navigation?WT.mc_id=partlycloudy-github-masoucou). That involved creating another class called `ArticleDetailPage.xaml` and using parameter binding to send information to it.

That's a wrap! Our application is starting to take on the look of the final application by using Xamarin.Forms Shell!

And we also get the benefit of a built-in navigation framework too - essentially, we get a bunch of functionality to make our app look great, act awesome, but we don't have to write a lot of the plumbing ourselves!

Keep on reading below for some more on how Shell passes parameters with Navigation binding, how you can style the `<TabBar>` in the Shell, and a bit more on the custom fonts that where used.

And next week we get into authentication with App Center Auth!

## Performing Navigation

`Shell.Current.GoToAsync` - there you have it.

No ... there's a lot more to it than that.

Shell can perform a lot of different types of navigation - and you can even stick with the regular Xamarin.Forms navigation framework if you like - but what I'm going to talk about here is sending parameters to a one page from another with Shell navigation binding.

### The Routing

For our purposes here, you can think of a route to a page as being similar to both a key/value and a relative URL

It's like a key/value because when we define our route we do so like this: `Routing.RegisterRoute("articledetail", typeof(ArticleDetailPage));`

So the string constant `articledetail` is the key that will always navigate to the value of `ArticleDetailPage`.

And you perform that navigation in shell with `Shell.Current.GoToAsync("articledetail");`

But it's like a relative URL because we can attach values at the end of the `articledetail` in query string format. Like a url.

Which means... YOU CAN [PASS DATA](https://docs.microsoft.com/xamarin/xamarin-forms/app-fundamentals/shell/navigation?WT.mc_id=partlycloudy-github-masoucou#pass-data) SUPER EASY!

So ... if I wanted to pass along the url for an article. I could create a query parameter named `articleUrl` and attach the url to it.

Then the full value for the route would look like this: `articledetail?articleUrl=THE-URL-GOES-HERE`. And I would pass that whole string to the `Shell.Current.GoToAsync()` function.

But how to read the article url value out?

There's a class attribute for that!

On the page which you are navigating to - in this case `ArticleDetailPage` - you'd decorate it with an attribute similar to: `[QueryProperty("ArticleUrl", "articleUrl")]`

That tells the page there will be a query string coming in with a key that has the name `articleUrl`. 

So take whatever the value is for that key - and pop it into a property of that page which would be called `ArticleUrl`.

Of course - we'd have to create that property too. And you can call the properties and query string parameters whatever you want - as long as you're consistent when navigating to the page.

## Styling the Shell

Even though Shell is very prescriptive on how it lays out tabs, and flyouts - it doesn't limit you. In fact you can [customize it a ton](https://docs.microsoft.com/xamarin/xamarin-forms/app-fundamentals/shell/configuration?WT.mc_id=partlycloudy-github-masoucou). In the video you saw how we hid the `NavigationBar`.

Here I want to show you how you can make the app on Android look good. Go ahead an run the Android version of the app now. It'll look like this:

![original Droid app](https://res.cloudinary.com/code-mill-technologies-inc/image/upload/e_shadow:40/v1572382972/1-allback_srijtv.png)

Ugh - look at that bottom tab bar! It's all black and you can't see the disabled tab button. Let's change that.

In the `AppShellPage.xaml` right below `Shell.TabBarColor="Red"` add this line: `Shell.TabBarBackgroundColor="White"`. If you're still running the app, hit save & XAML Hot Reload 🔥🔄will update the screen automatically for you. It'll look like this now:

![Android app with white tab bar](https://res.cloudinary.com/code-mill-technologies-inc/image/upload/e_shadow:40/v1572382972/2-whiteback_beh5fc.png)

That's a bit better - but we still can't see the disabled tab bar button. So add this right after the line you just did: `Shell.TabBarDisabledColor="Gray"`.

Now we have:

![Android with gray disabled tab bar buttons](https://res.cloudinary.com/code-mill-technologies-inc/image/upload/e_shadow:40/v1572382972/3-graydisabled_l5ds8k.png)

![Android with gray disabled tab bar buttons - view 2](https://res.cloudinary.com/code-mill-technologies-inc/image/upload/e_shadow:40/v1572382972/4-graydisabled_hlkydv.png)

Yeah! Now this app is starting to look pretty darn good!

## Adding Custom Fonts

One thing that we did during the video was use a special font that had glyphs in it. A font that you may already be familiar with that does this is Font Awesome.

In this episode I used Segoe MDL 2. Same idea, different font.

You can get more info on that font, including the ability to [download it here](https://docs.microsoft.com/windows/uwp/design/style/segoe-ui-symbol-font?WT.mc_id=partlycloudy-github-masoucou#about-segoe-mdl2-assets).

[This here has all the in-depth goodness](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/text/fonts?WT.mc_id=partlycloudy-github-masoucou#display-font-icons) on Forms with Font Icons, but the quick version - keep on reading!

To get access to all the glyphs in that font - add it to the `Assets` folder in your Android project. And add it to the `Resources` folder in your iOS project.

Also on iOS, you'll need to edit your `info.plist` file. Add a key `UIAppFonts` with an `<Array>` value that has a single `<String>` entry of the font's name on disk. Or most like here `SegMDL2.ttf`.

Then you'll need to do something special in the `App.xaml` file in order to refer to the font later on.

Getting at the font is different in iOS vs Android. So the easiest way to handle that is to create an entry in the application's `<ResourceDictionary>`.

Make it look like this:

```language-xaml
<ResourceDictionary>
  <OnPlatform
      x:Key="SegMDL2"
      x:TypeArguments="x:String"
      Android="SegMDL2.ttf#Segoe MDL2 Assets"
      iOS="Segoe MDL2 Assets"
  />
</ResourceDictionary>
```

The difference is what the font is named on each platform. But by doing it as a static resource - you can just refer to it by `{StaticResource SegMDL2}` from here on out.

Then anytime you need to display a glyph - you can just use a `<FontImageSource>`. Setting the `FontFamily` and `Glyph` properties appropriately.

This is what the moon looks like:

`<FontImageSource FontFamily="{StaticResource SegMDL2}" Glyph="&#xE708;" />`

You can find all the `Glyph` values for SegoeMDL 2 on the page I linked to above. One thing to remember is that you'll have to prefix the values with `&#x` to get them to work.

And that's it!

We're on our way to a great looking app thanks to Shell and a little bit of `<FontImageSource>` magic!
