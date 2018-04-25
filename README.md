# Yet another weather service

* Self-hosted *RESTful* service built with *Asp.Net WebApi*.
* The API has *two endpoints* - weather for today, weather for the whole week - and accepts city as a parameter. Build the data structures which you find suitable. 
* Anonymous authentication, open to everyone. No additional security requirements.
* You can use some predefined data as a data source or if you have enough time and interest feel free to use any existing Weather API service to pull the actual data.
* *No specific database storage* required, feel free to store the data wherever you want (in memory, in a file, etc.)
* In order to prevent request flood we introduce customizable rate limits. *Each API endpoint* should have its own rate limit settings defined by its own *extendable set of rules* which are *based on user's IP address*. If *any of applied rules do not pass* return rate limit exceeded HTTP error instead of doing actual work. 
* It should be easy to implement and enforce rules to API endpoints without modifying much code. Feel free to choose the approach how you'd implement this extensibility requirement. 
* Example rate limit rules to implement: N allowed endpoint requests per IP address per selected period, N minutes passed since previous user's request.
* Implement any *tests (unit, integration)* you consider valuable and reasonable.
* If you need any NuGet packages feel free to use them.
* Imagine this is a real production code you used to write and apply all the principles you use during your daily routine.
Should you have any questions, please do not hesitate to create a github [Issue](https://github.com/sviataslau/crexi/issues) where we can address all the controversial points.
* Please, make a fork of the repository, start an *own branch named 'Your-Name-implementation'* and submit your *changes as a PR* when you're ready 
Happy coding and good luck!
