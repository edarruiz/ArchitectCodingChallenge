# .NET Architect Coding Challenge

> *Hello reader!*
>
> *I prepared this document to guide you through my test.*
>
> *Here you will find all information related to the solution I provide for this test.*
>
> *I'd like to thank you for your time, and for your attention while reading my code and my documentation. I did my best to provide you a good experience while evaluanting my test.*
>
> *I hope you find it interesting! If you need any more information, feel free to ask me! If you also have any sugestions which could help me to improve, please let me know!*
>
> *Kind regards,*
> *Eric*

This repository contains a proposed solution for test of .NET Architect Coding Challenge described in the following:

- [.NET Architect Coding Challenge](#net-architect-coding-challenge)
  - [1. Abstract: The problem](#1-abstract-the-problem)
  - [Answer 1](#answer-1)
  - [Answer 2](#answer-2)
  - [Answer 3](#answer-3)
  - [Additional information](#additional-information)
  - [2. My business perspective, design thinking and decision making for this solution](#2-my-business-perspective-design-thinking-and-decision-making-for-this-solution)
  - [3. My technology perspective, design thinking, architecture and decision making for this solution](#3-my-technology-perspective-design-thinking-architecture-and-decision-making-for-this-solution)
  - [5. Conclusion](#5-conclusion)



## 1. Abstract: The problem
From the original e-mail:

*BairesDev specializes in delivering technology solutions. One of our engagement models is IT Staff Augmentation, where our engineers are an extension of the client’s teams. Most of our developers (approx 95%) are located in LATAM.*

*We are looking to expand our client’s portfolio. We request you to develop a .NET Core RESTful API that, given an input file “people.json” with LinkedIn public data, conforms to the following Implementation:*

*1) An endpoint that finds the N people with the highest chance of becoming our clients, being N a parameter, as a JSON list of PersonId.*

*Example:*
*<code> http://....../topclients/2 → Response: [{“PersonId”:150},{“PersonId”:985}]</code>*

*2) An endpoint that finds, for a given PersonId, the position on the priority potential clients list.*

*Example:*
*<code>http://....../clientposition/150 → Response: {“Position”:1}</code>*

***Bonus implementation:** Another endpoint that allows the insertion of a new Person object and calculates its priority value.*

*The input file is a JSON array of objects structured like the following example:*

```json
[
{
"PersonId": 4580, // long
"FirstName": "Jhon", // string
"LastName": "Smith", // string
"CurrentRole": "co-founder & cto", // string
"Country": "Germany", // string
"Industry": "United States", // string
"NumberOfRecommendations": 10,// int nullable
"NumberOfConnections": 500 // int nullable
},
{
....
}
]
```
## Answer 1
For this enpoint, I will create a <code>GET</code> method that will do the following:
1. Load the dataset to an In-memory database, from the <code>people.json</code> file provided;
2. Query all the data and filter the person's by its current <code>CurrentRole</code>, which needs to contains the word <code>developer</code> - which I defined being the key value to the ranking;
3. Sort the results <code>ascending</code> by the field <code>PersonId</code>;
4. Return all the results found as <code>application/json</code> within a <code>HTTP CODE: 200 (Ok)</code>.

## Answer 2
For this endpoint, I will create a <code>GET</code> method what will do the following:
1. Load the dataset to an In-memory database, from the <code>people.json</code> file provided;
2. Query all the data and filter the person's by its <code>PersonId</code> provided by the <code>query parameter</code> named <code>N</code>;
3. Return results as <code>application/json</code> within the following <code>HTTP CODES</code>:
   1. <code>200</code> - return the person's information when the person's <code>PersonId</code> exists.
   2. <code>404</code> - return no results when the person's <code>PersonId</code> does not exists.

## Answer 3
For this endpoint, I will create a <code>POST</code> method that will do the following:
1. Receive the information inside the request <code>body</code>, as <code>application/json</code>;
2. Do the validations based on the provided data types, required and optional fields;
3. Load the dataset to an In-memory database, from the <code>people.json</code> file provided;
4. Add the new person to or a list of new persons to the In-memory dataset;
5. Save the changes to the <code>people.json</code> file and write it to disk;
6. Return a <code>HTTP CODE: 200 (Created)</code> with the results.
   1. If the person already exists in the list, the operation will fail and return with the message *Failed: This PersonId already exists in the list*.
   2. If the person does not exists in the list, will be added and return the message *Created successfully*.
Example:
```json
[
    {
        "PersonId": 4580,
        "StatusCode": 400,
        "Message": "Failed: This PersonId already exists in the list."
    },
    {
        "PersonId": 646171916,
        "StatusCode": 400,
        "Message": "Failed: This PersonId already exists in the list."
    },
    {
        "PersonId": 458000,
        "StatusCode": 201,
        "Message": "Created successfully."
    },
    {
        "PersonId": 646171916000,
        "StatusCode": 201,
        "Message": "Created successfully."
    }   
]
```

## Additional information
You do not need to worry how the application loads the dataset from the json file. I put the <code>people.json</code> file as an embedded resource inside the application layer, so the API handles the file system needed to persist and query the required data. For this task I created a custom In-memory database implementation based on this test needs.

I did not implemented the unit tests for all classes existing in the codebase, but to demonstrate I know how to use the patterns and the good pratices, I've implemented a lot of unit tests for the core classes, for example, inheritance core classes, domain core classes and infrastructure core classes, using both traditional unit tests and also using mocks and fakes.

## 2. My business perspective, design thinking and decision making for this solution
Every good software solution is always preceeded by a good understanding of the bussines needs, their goals, the market rules and the end-user expectations.

With this in mind, here I bring are some of the design thinking process I had, pointing out the reasons behind each business decision was made to implement and complete this challenge:

> **Thinking:** After analysing the datasource provided, I discovered the main problem pointed in the questions was not creating a rank the people with the highest chance of becoming our clients, listing and adding them.
> 
> The tricky business problem is somewhat hidden behind the lines, where I needed to UNDERSTAND and ASK: 
> 
> *From all information I had on this LinkedIn people dataset, what information is provided that I could find a diferentiation between them, and what information provided could be easily seen as a clear link for the company's vision, which is "our engineers are an extension of the client’s teams"?* - **I see this as a business target for this test.**
>
> After some querying and evaluation throughout the data, I ended up with some conclusions:
> - There is no explicit filter or information inside the data that show what criteria to use when deciding *"who have the highest change of becoming the company client"*, as well there is no explicit information pointed in the question itself that helps me to find what information to use to create the ranking mechanism. 
> - **But, if I carefully read and try to understand the domain of the information**, which is the people, being a collection of persons, being each person a possible professional the company could hire, there are some specific information that should help me to answer this question.
> - I found the key informations are  their **current role**, existing in every person entity information;
> - I still need to decide exactly what values I need to filter to create a ranking. After analysing all the information available for each of this attribute, I can choose which *current role* I should prioritize, resulting in the expected ranking. Since the question also is not explict about these specific values or the values I need to use, so I feel free to decide which information will compose the ranking, being in this case, **any value containing the word <code>developer</code>.**
> 
> **Decision making:**
> Once I got a good understanding of the information domain, the choosing information composing the ranking will be the following:
> 
> |Key|Objective|Ranking|Reasons|
> |---|---|---|---|
> |**PersonId**|The unique identifier of the person|:heavy_multiplication_x:|Only used by indexing, preseting and querying. Does not represent a weight in the rank|
> |**FirstName**|The person's name|:heavy_multiplication_x:|Only used for querying and presenting. Does not represent a weight in the rank.|
> |**LastName**|The person's last name|:heavy_multiplication_x:|Only used for querying and presenting. Does not represent a weight in the rank.|
> |**CurrentRole**|The person's current role|:white_check_mark:|The current role is the most important information for the company hiring process. It should give me the set of values for the ranking composition.|
> |**Country**|The person's country origin|:heavy_multiplication_x:|Since the company hires world-wide, it's not relevant for the ranking. Only used by indexing, presenting and querying. Does not represent a weight in the rank|
> |**Industry**|The person's industry|:heavy_multiplication_x:|The industry of the person is the second most important information for the company hiring process. But will not be used right now for filtering.|
> |**NumberOfRecommendations**|The person's # of LinkedIn recomendations|:heavy_multiplication_x:|The number of recomendations of a person is a good indicator, but also not reliable, since there is a lot of good professionals with no good recomendations. Could be used as an additional ranking set of values, but for now, it'll be discarded in our ranking. |
> |**NumberOfConnections**|The person's # of connections|:heavy_multiplication_x:|The number of connections is a measure "how *well connected*/*connected* this person is". If we think in higher positions, for example, management, high leadership and c-level, maybe we should consider this, but anyway, its too specific at this moment. For now, it'll be discarded in our ranking.|

## 3. My technology perspective, design thinking, architecture and decision making for this solution
After understanding all business goals and needs, then I can proceed to start the discovery process of the technical solutions available at my disposal to reach the expected results. This is a crucial process.

With this in mind, here I bring are some of the design thinking process I had, pointing out the reasons behind each technical decision was made to implement and complete this challange:

> **Thinking:** As a software architect and a developer myself, I need to choose the correct architecture and enforce the design patterns needed. Also, its is crucial to determine what path of concepts I should or should not be using it for each goal. These goals are usually simple, listed as follows, not in any particular order: 
> - Write only the necessary code;
> - Write efficient code;
> - Make it testable as soon as possible;
> - Always log, always trace;
> - For the errors, handle every possible known error
> - For the unknown errors, handle them automatically, preventing the application to halt or shutdown;
> - Be alert and always be careful with performance while implementing the code, which include selecting the correct types, algorithms and implementations that should not be responsible for creating throttling or excessive usage of resources on the machine or environment where the system will run;
> - Be pragmatic, both for the application structure and its data structure;
> - Don't be passionate about a single idea or concept: be open to new solutions for older and new problems;
> - Document everything, from the source-code to the exposed API;
> - Always aim information security, be carefull with exploits and security breaches;
> 
> **Decision making:** I made some architectural decisions that will define exactly what should be found my test, and will be implemented in the final code:
> 
> - I will use only the design patterns needed, priorizing quality and clearity over complexity and future possibilities when designing the API;
> - Domain-Driven Design will be used: since I'm dealing with only one Entity, there is no need for Aggregates and Aggregate Roots, so I'll cut them off, making the Domain simpler. Value Objects, Result objects and another pre-requisites will be implemented if needed;
> - I will use the CQRS (Command and Query Responsability Segregation) pattern;
> - I will use the Mediator pattern;
> - Validation will be done functionaly using the FluentValidation framework;
> - The project will follow the Microservices Architecture infrastructure model;
> - The project will follow the Clean Architecture as its architectural design model;
> - TDD will be used for testing using the XUnit framework;
> - All excess components of the architecture that was not used will be cut, for example, Domain Events, Persistence to databases inside the Infrastructure layer, Messaging Broken, Message Bus, real time events, etc. These parts make the solution as a whole when looking for an implementation in the real world - which is not our case for this test purposes.
> - I will create a custom implementation of an *in-memory dataset to perform the required tasks**, which will load the json file in the system memory, and save it to disk if any changes were made.
> - I will use Mocks and Fakes to improve the test coverage of the codebase. When needed I will use wrappers to provide unit testing funcionallity where could be dificult to implement unit tests, for example, when using a file system to access data;
> - I will use exceptions only when absolutely necessary, improving the application performance by a large margin. Other than that, errors will be handled by Result Objects and custom error message objects.
> - For the security layer, I could use *Authentication* and *Authorization*, but it won't be implemented since its not a requirement.
> 
> As for the tools, I selected:
> - **Visual Studio 2022 Community, version 17.7.1** will be used to code and implement this solution;
> - **ASP.NET Core with .NET 7.0 (STS)** will be used as the target framework;
> - **C#, version 11** will be used as the language version for this solution;
> - **Docker** scripts will be available for deploying the application as a container;
> - **Controllers** will be used instead of minimal APIs for this solution;
> - **OpenAPI** will be supported with Swagger for API documentation, also, it will need to be authenticated to proceed with the Requests;
> - **Top-level statements** will be turned on for this solution;
> - **MediatR, version 12.1.1 (package)** will be used as a framework for the Mediator Design Pattern;
> - **FluentValidation, version 11.7.1 (package)** will be used as framework for strongly-type validation rules;
> - **Xunit, version 2.5.0 (package)** will be used as framework for the unit test in TDD;
> - **FluentAssertions, version 6.11.0** will be used as framework for the test unit assertions in a funcional way;
> - **FluentResults, version 3..15.2** will be used for the Result Objects and dealing with the domain business rules instead of throwing exceptions;
> - **Moq, version 4.20.69** will be used for mocking on unit testing;
> - **Serilog, version 3.0.1** will be used as framwork for logging;

## 5. Conclusion
I expect my implementation fulfill your expectations, as well, provide you the desired technical results you are looking for.

If you have any questions or need any additional information or explanation, feel free to contact me. I'll be available and ready.

<code>#DevRegards</code>
<code>#ItWorks</code>
<code>#Done.</code>