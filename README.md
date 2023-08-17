# .NET Architect Coding Challenge

> *Hello reader!*
>
> *I prepared this document to guide you through my test.*
>
> *Here you will find information not only about the solution I provide, but also about the process I went throught to think, decide, evaluate, create, implement, test and document every part required to provide the results of this test.*
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
  - [2. A Business Perspective: About my design thinking and decision making](#2-a-business-perspective-about-my-design-thinking-and-decision-making)
  - [3. A Technology Perspective: About my design thinking, architecture and decision making](#3-a-technology-perspective-about-my-design-thinking-architecture-and-decision-making)
  - [4. The Results: Tests and validation process](#4-the-results-tests-and-validation-process)
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
## 2. A Business Perspective: About my design thinking and decision making
Every good software solution is always preceeded by a good understanding of the bussines needs, their goals, the market rules and the end-user expectations.

With this in mind, here I bring are some of the design thinking process I had, pointing out the reasons behind each business decision was made to implement and complete this challenge:

> **Thinking and understanding:** After analysing the datasource provided, I discovered the main problem pointed in the questions was not creating a rank the people with the highest chance of becoming our clients, listing and adding them.
> 
> The tricky business problem is somewhat hidden behind the lines, where I needed to UNDERSTAND and ASK: 
> 
> *From all information I had on this LinkedIn people dataset, what information is provided that I could find a diferentiation between them, and what information provided could be easily seen as a clear link for the company's vision, which is "our engineers are an extension of the client’s teams"?* - **I see this as a business target for this test.**
>
> After some querying and evaluation throughout the data, I ended up with some conclusions:
> - There is no explicit filter or information inside the data that show what criteria to use when deciding *"who have the highest change of becoming the company client"*, as well there is no explicit information pointed in the question itself that helps me to find what information to use to create the ranking mechanism. **But, if we carefully read and try to understand the domain of the information**, which is the people, being a collection of persons, being each person a possible professional the company could hire, there are some specific information that should help me to answer this question. From all available information, I found the key informations are  their **current role** and their **industry**, existing in every person entity information;
> - Even with this first two filters as key informations, I still need to decide exactly what information content I need to index to create a ranking. After analysing all the information available for each of this two attributes, I can choose which *current role* and which *industry* I should prioritize, resulting in the expected ranking. Since the question also is not explict about these specific values or the values I need to use, so I feel free to decide which information will compose the ranking. 
> - I concluded I need to consider for the company hiring purposes, that people with *current roles* and *industry* related to technology, HR and management should be considered a higher priority targets. For the sake of simplicity, I'll be considering only people for the IT purposes, hiding the HR, management and C-level as possible targets.
> 
> **Decision making and designing:**
> Once I got a good grip of the information domain, the choosing information composing the ranking will be the following:
> 
> |Key|Objective|Ranking|Reasons|
> |---|---|---|---|
> |**PersonId**|The unique identifier of the person|:heavy_multiplication_x:|Only used by indexing, preseting and querying. Does not represent a weight in the rank|
> |**FirstName**|The person's name|:heavy_multiplication_x:|Only used for querying and presenting. Does not represent a weight in the rank.|
> |**LastName**|The person's last name|:heavy_multiplication_x:|Only used for querying and presenting. Does not represent a weight in the rank.|
> |**CurrentRole**|The person's current role|:white_check_mark:|The current role is the most important information for the company hiring process. It should give me the set of values for the ranking composition.|
> |**Country**|The person's country origin|:heavy_multiplication_x:|Since the company hires world-wide, it's not relevant for the ranking. Only used by indexing, presenting and querying. Does not represent a weight in the rank|
> |**Industry**|The person's industry|:white_check_mark:|The industry of the person is the second most important information for the company hiring process. It should give me the set of values for the ranking composition.|
> |**NumberOfRecommendations**|The person's # of LinkedIn recomendations|:heavy_multiplication_x:|The number of recomendations of a person is a good indicator, but also not reliable, since there is a lot of good professionals with no good recomendations. Could be used as an additional ranking set of values, but for now, it'll be discarded in our ranking. |
> |**NumberOfConnections**|The person's # of connections|:heavy_multiplication_x:|The number of connections is a measure "how *well connected*/*connected* this person is". If we think in higher positions, for example, management, high leadership and c-level, maybe we should consider this, but anyway, its too specific at this moment. For now, it'll be discarded in our ranking.|

## 3. A Technology Perspective: About my design thinking, architecture and decision making
Once we already have a good grip of all business goals and needs, then we can proceed to start the discovery process of the technical solutions available at our disposal to reach the expected results. This is a crucial process.

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
> - For the security layer, there will be using *Authentication* and *Authorization* within the specific credentials:
> 
> |User Name|Password|Roles|Query by Ranking?<br><sub>Question 1 (API 1)</sub>|Query by position Priority<br><sub>Question 2 (API 2)</sub>|Add new person?<br><sub>Bonus implementation</sub>|
> |---|---|---|---|---|---|
> |<code>admin</code>|<code>ArchitectCodingChallenge@ADMIN#123|<code> RankingReader<br>RankingWriter<br> Admin</code>| :white_check_mark: | :white_check_mark: | :white_check_mark: |
> |<code>developer</code>|<code>ArchitectCodingChallenge@DEVELOPER#123</code>|<code>RankingReader</code>|:white_check_mark:|:white_check_mark:|:heavy_multiplication_x:|
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
> - **FluentResults, version 3..15.2** will be used for the Result Objects and dealing with the domain business rules instead of throwing exceptions. 

## 4. The Results: Tests and validation process
Now is time to put all things together and show the test results! 

Once fully implemented, with the written unit tests and the test cases, I can show the results here, and the tests are available for any execution at any given time for you guys. This will prove the solution I create provide the actual behavior and expected results from the required implementation tasks of this project.

Also, as a proof of the concept, I'm adding pictures as evidences of the system working and running, for both two expected results: one for sucesses and at least one for error.

TODO: PRINT SUCCESS API 1
TODO: PRINT ERROR API 1

TODO: PRINT SUCCESS API 2
TODO: PRINT ERROR API 2

TODO: PRINT SUCCESS API 3
TODO: PRINT ERROR API 3

## 5. Conclusion
I expect my humble implementation fulfill your expectations, as well provide for you the desired technical results you are looking for.

Participating in this selection process is being a joy and an awesome personal experience, so I thank you and congratulate every person I had contact with since my first steps applying for this position!

If you have any questions or need any additional information or explanation, feel free to contact me. I'll be available and ready.

<code>#DevRegards</code>
<code>#ItWorks</code>
<code>#Done.</code>