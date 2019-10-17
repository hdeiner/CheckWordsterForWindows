[![Build Status](https://travis-ci.com/hdeiner/CheckWordsterForWindows.svg?branch=master)](https://travis-ci.com/hdeiner/CheckWordsterForWindows)

This project is about building microservices and properly testing them.

- The microservice is a simple one: take a string of characters for money and translate them into the corresponding string of words that you would put on a check.
- The code is built with unit tests. So that everyone can trust that the code is written well.
- The code is then tested with Gherkin statements, so other stakeholders can understand that the code does what it's supposed to do.
- The microservice is then tested against a fake server (WireMock, which allows beautiful URL request matching and response generation, and breaks the client/server dependency).  WireMock is brought up in a Docker container, for platform independence and the beauty of not having to install anywhere.
- And, finally, the same Gherkin tests are run against a locally hosted server.