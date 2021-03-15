# BusinessDayCounter Class Library

This is a [.NET Standard 2.0](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md) class library written in [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) that provides methods to count weekdays and business days between two given dates. The implementation relies on some assumptions and there are certain limitations.

## Solution Design

This sample library has been developed for the purpose of a technical challenge and has usage limitations.

Given the requirements, I've chosen to write a [class library](https://docs.microsoft.com/en-us/dotnet/standard/class-libraries) with the aim that it can be shared and used by other applications. 

Using [.NET Standard 2.0](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md) enables [support](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) for previous versions of [.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework) and newer ones like [.NET Core](https://dotnet.microsoft.com/download/dotnet)

For simplicity and based on the purpose of this library, all classes have been kept in the same folders except for the Helper class.

## Assumptions

We assume that the local CultureInfo is following the Australian standard.

There are two ways to define public holidays.
- By supplying a list of public holidays as dates
- By supplying a list of public holidays rules

We assume that the expected behavior of the application is correct strictly based on how public holidays are defined and supplied to the library.

The library is not responsible for verifying if a public holidays defined in a list or rules is a actually a public holidays in the real world.

## Library Usage

Clone the following repository:

BusinessDayCounterProject:  https://github.com/StephaneChevassus/BusinessDayCounterProject

BusinessDayCounter is a static class that exposes all the public methods available such as:

- WeekdaysBetweenTwoDates: Caculates the number of weekdays between two dates
- BusinessDaysBetweenTwoDates: Caculates the number of business days between two dates given a list of public holidays
- BusinessDaysBetweenTwoDates: Caculates the number of business days between two dates given a set of rules that define public holidays

## Testing

I've implemented [xUnit Tests](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test) using .NET Core 3.1 to validate that the application is working as expected based on the requirements provided.

To run the tests from [Visual Studio 2019](https://www.visualstudio.com/vs/):

1. Open the solution in your IDE (using the _BusinessDayCounterProject.sln_ solution file).
2. Open the Test Explorer
3. Click the _Run All_ link in the Test Explorer window, and you should see the results update in the Test Explorer window as the tests are run:

![Test Explorer Window with the results](/static/screenshots/Test-Explorer-Screenshot.png "Test Explorer Window with the results")

Check [these instructions](https://xunit.net/docs/getting-started/netcore/cmdline#run-tests-visualstudio) to get more information on how to run the tests.

## Future Improvements

If I had more time I would have added a couple of extra tests to cover all DayCountHelper methods.

For Task 3, the public holidays generated from the rules are not strictly within the date range provided but it does not cause any side effects to have extra dates in the list of public holidays.
I would have added a step to check that the current date is within the date range before adding it to the list.

The PublicHolidaysRules could be implemented in multiple ways which would mostly depend on broader requirements than just having 3 types of public holidays as it exists more. I could have tried to make the creation of each public holidays more dynamic within each rule instead of hard coding the actual public holiday such as:
- AnzacDay
- QueensBirthday
- NewYearsDay

I would have liked to add a couple of extra public holidays to demonstrate how easy it is to add more of them without modifying the behavior of each the class.

If such library was to be designed for handling real public holidays at a global level, here are some important requirements that would need to be implemented:
- Calendar type based on Culture
- Date format based on Culture
- Country
- States
- etc..