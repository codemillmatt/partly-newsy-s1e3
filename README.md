# Partly Cloudy Episode 3: If Forms Was a Turtle (Xamarin.Forms Shell)

Welcome back to Partly Cloudy! The show where you learn how to build a cloud-connected Xamarin mobile application. We start from nothing and don't quit until it's ready for the App Store!

In this episode we start to make our app look a little bit more like the finished one! We go from one that only displays headlines in a `ListView` to one that supports top and bottom tab, displays graphics in those tabs, and can perform navigation between different views of the app.

And all of this is done with a Xamarin.Forms feature called Shell.

## Episode Recap

We're building a clone of the Microsoft News app. 

### Past Episodes 

In [episode 1](https://channel9.msdn.com/Shows/Partly-Cloudy/Hello-News-Intro-project-structure-and-HTTP-requests?WT.mc_id=partlycloudy-c9-masoucou) we setup the project structure and started to call an Azure Function.

Then in [episode 2](https://channel9.msdn.com/Shows/Partly-Cloudy/Inform-Me-Bing-News-API?WT.mc_id=partlycloudy-c9-masoucou) we made that Azure Function invoke the Bing News Search API, so we could view the news in our app.

### What Happened in This Episode

In this episode we give our user interface some structure through [Xamarin.Forms Shell](https://docs.microsoft.com/xamarin/xamarin-forms/app-fundamentals/shell/?WT.mc_id=partlycloudy-c9-masoucou).

#### Xamarin.Forms Shell

Shell reduces the amount of work that you have to do in order to creae a full-featured application. It provides a scaffolding to build tabs, flyout, navigation - including the ability to pass parameters via a URL based syntax, and more.

In other words - Shell is a prescriptive means of developing apps that saves you time by providing features for you so you don't have to development them from scratch over and over again.

#### The Play-by-Play

If you're building the app along with the show, download the code in this here repo!

You'll have to add a `local.settings.json` file to the Azure Functions project to make sure it can communicate to the Bing News Search API, but after that, you'll be good to go. [Here's a starter one](https://gist.github.com/codemillmatt/828ace7089a93fccd0ac4012e006d9a4) for you.

1. The first thing we did in the episode was to add a new `Pages` folder - and added a new `ContentPage` to it called `NewsCollectionPage.xaml`. Copied all the functionality over from the `MainPage.xaml` into it. This is setting us up for a more proper structured project long-term.
1. Then we introduced Shell by adding a page called `AppShellPage.xaml`.

This is the root page for Shell applications and everything is enclosed within `<Shell></Shell>` tags.

3. From there we added a `<TabBar>` element. This will allow the `NewsCollectionPage` created above to sit within a tab at the bottom of the Shell.

So our Shell page starts to look like this:

<script src="https://gist.github.com/codemillmatt/e9f7c564924cdef3ddaac9128fee57ee.js"></script>

One thing to note - if a `<TabBar>` element only has one `<Tab>` in it - it will _not_ display any tabs at all.

> If a `<TabBar>` only has one `<Tab> in it, it will not display any tabs at all.

4. By adding more `<Tab>` elements with pages within the `<ShellContent>` we were able to see more pages being added.

5. Next we wanted to add top tabs to the page. The way you do this is to add multiple `<ShellContent>` elements within a _single_ `<Tab>` element. 

This is telling Shell - hey - I want a bunch of sub pages in here - so Shell says - let's put them along the top!

<script src="https://gist.github.com/codemillmatt/c805ec773676d8ee34f32a4f251abf4b.js"></script>

6. Then we spiced things up by adding graphics to the tabs along the bottom of the page.

We did this with glyphs from a special font ... kinda like Font Awesome. And the `FontImageSource` object.

You can use regular images too if you wanted to. But the cool thing about using a font is that you get a ton of images .. I mean glyphs all built into a single package, if you will.

7. Then finally we did some navigation, using Shell's built-in navigation framework. That involved creating another class called `ArticleDetailPage.xaml` and using parameter binding to send information to it.

That's a wrap! Our application is starting to take on the look of the final application by using Xamarin.Forms Shell!

And we also get the benefit of a built-in navigation framework too - essentially, we get a bunch of functionality to make our app look great, act awesome, but we don't have to write a lot of the plumbing ourselves!

Keep on reading below for some more on how Shell passes parameters with Navigation binding, how you can style the `<TabBar>` in the Shell, and a bit more on the custom fonts that where used.

And next week we get into authentication with App Center Auth!

## Performing Navigation

`Shell.Current.GoToAsync` - there you have it.

No ... there's a lot more to it than that.

## Styling the Shell

## Adding Custom Fonts