# Final Exam Project At JobsterMedia B.V.
## Testing new project and fixing project dependencies on end to end and unit tests

*Unfortunately i am not able to share the code, as it is company's policy that the code is private. What i am able to do is give examples of the of self written code, explain what i learned, and how i approached problems.

### 
 A complete documentation of my internship can be found in this document: https://docs.google.com/document/d/1sUNfNJ6Kdsq5yRrHQNPRYZ6z4zaqvuq3h6AtAieyw2E/edit?usp=sharing

 Presentation slides from the exam can be found here: https://docs.google.com/presentation/d/1h4ED2pbCLGW83rqh6aqgE04INJs6DHyNZnTD9rcRxCo/edit?usp=sharing



## Problem Statement 

As the functionality from the main project is being changed and moved to the new Nuxt project, a lot of problems appear for both projects. The first one - the new Nuxt project is untested. The second one - tests that have covered old functionality in the main project are now failing. This also means that all of GitLab's pipelines are failing. Developers are unable to receive any useful feedback from the pipelines. In addition to that, deployment is also not possible, as developers cannot know if the tests are failing because some features include bugs or because the features are no longer in the codebase. A lot of new questions appear when we try to fix these problems, these questions need to be addressed and implemented before production release:


 * Which framework should be used for a new project based on Nuxt, Vue.js, and TypeScript technologies?
 * How tests should be implemented to assure code quality and readability by other developers?
 * How should tests be run in Gitlab CI pipelines for quality assurance and deployment?
 * How should Unit and End to End tests be fixed after moving most of the functionality and logic to another project?


## Conclusion 


The aim of this project was to choose and set up a testing framework for new SPA project. Followed by implementing tests for classes, store, and components and trying to reach over 50% code coverage. Another step was to use the SPA project during End-To-End tests on the main project in GitLab CI. With this various different pipelines had to be created. Finally, tests on the main project had to be fixed. I believe during the course of this project which was about 10 weeks I managed to achieve and solve all of the problems that were given to me. 

To fix PHPUnit tests in the main project, we need to understand that the responsibility of the code changed. Some logic or frontend display was moved to a new SPA project, we need to imagine that the SPA project works perfectly fine and that it is enough to just test what is passed to the Request. Mocking this Request class will allow us to flawlessly test it and fix breaking tests. For End-To-End tests, we still have the same more or less features as before, mainly the looks of the elements have changed, therefore we just need to update CSS selectors, add ‘sleep()’ function, and update minor features to get tests working again.

For the SPA project, we need to choose a library, this is a very important step, as we wouldn’t want to start changing libraries or start refactoring all of the tests, just because we made a bad decision when picking a library. We need to make sure that the library is planned to be maintained in the future, that documentation is clean, help is available on the internet, and that we can stick to our coding standards. For this exact project, the Framework that matches all of these points is Jest. As it is currently the most used testing framework, it was developed by Facebook developers and has been growing in interest in the past 10 years. It has great documentation and funcionalities such as synchronous testing.

To set up Jest for SPA we need Vue-Test-Utils, which adapts Jest for our Vue-based project and has already writen documentation on how to migrate tests when migrating framework form Vue 2 to Vue 3. With this installed, we are able to tackle classes once by one, starting from components, then going through the store, and other than other classes. It is also important to mock classes and know the responsibilities of each class. Overall to assure code quality and achieve readability, we can create test-helper classes, mock external classes, understand classes responsibilities, and do proper documentation.

Finally, to fix GitLab’s pipelines and End-To-End tests, we should be starting up both projects at the same time. With this, we are not mocking anything and allowing to test the real user experience. It requires some advanced setup in gitlab-ci configuration files, which uses docker  containers. The project should be built and production node packages should also be installed before pushing container to the GitLab’s Container Registry to speed up tests and avoid timeout issues. This could also improve the debugging as developers are able to pull these containers and start working within minutes. We can also optimize some flaws of TypeScripts single-threaded behaviour with PM2 library. This will allow to run projects on each core of the production machines. 
