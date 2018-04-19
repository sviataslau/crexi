# Yet another weather service

* Self-hosted *RESTful* service built with *Asp.Net WebApi*.
* The API has *two endpoints* - weather for today, weather for the whole week - and accepts city as a parameter. Build the data structures which you find suitable. 
* Anonymous authentication, open to everyone. No additional security requirements.
* You can use some predefined data as a data source or if you have enough time and interest feel free to use any existing Weather API service to pull the actual data.
* *No specific database storage* required, feel free to store the data wherever you want (in memory, in a file, etc.)
* *Each endpoint* should be rate limited by its own *extendable set of rules* which are *based on user's IP address*. 
* If any of applied rules do not pass return rate limit exceeded HTTP error instead of doing actual work. 
* It should be easy to implement and enforce rules to API endpoints without modifying much code. Feel free to choose the approach how you'd implement this extensibility requirement. 
* Example rate limit rules to implement: X endpoint requests per IP address per selected period, X minutes passed since previous user's request.
* Implement any *tests (unit, integration)* you consider valuable and reasonable.
* If you need any NuGet packages feel free to use them.
* Imagine this is a real production code you used to write and apply all the principles you use during your daily routine.
Happy coding and good luck!
