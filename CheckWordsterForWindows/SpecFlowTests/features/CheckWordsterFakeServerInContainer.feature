Feature: CheckWordsterFakeServerInContainter

  For the hipster bank customer
  Who is unhappy with traditional bank check issuing services, because the checks written don’t have that retro feel,
  The CheckWordster is a microservice of a bank check writing service
  That provides the words for the amounts on a check that used to be written by people.
  Unlike ECPS (Enterprise Check Production System),
  Our product gives checks that old time feeling, with amounts in both numbers and words.

  Background: Start server
    Given I start the "wiremock-container" server

  Scenario Outline: Convert numbers into words when all goes well
    When I convert "<number>" into words
    Then it should be "<words>"

    Examples:
      |number       |words                                                                                                                                 |
      |22           |Twenty two                                                                                                                            |
      |40           |Forty                                                                                                                                 |
      |65           |Sixty five                                                                                                                            |
      |256          |Two hundred fifty six                                                                                                                 |
      |770          |Seven hundred seventy                                                                                                                 |
      |900          |Nine hundred                                                                                                                          |
      |1000         |One thousand                                                                                                                          |
      |1,000        |One thousand                                                                                                                          |
      |1954         |One thousand nine hundred fifty four                                                                                                  |
      |19542        |Nineteen thousand five hundred forty two                                                                                              |
      |319542       |Three hundred nineteen thousand five hundred forty two                                                                                |
      |319540       |Three hundred nineteen thousand five hundred forty                                                                                    |
      |319500       |Three hundred nineteen thousand five hundred                                                                                          |
      |1,000,000    |One million                                                                                                                           |
      |1,100,000    |One million one hundred thousand                                                                                                      |
      |1,110,000    |One million one hundred ten thousand                                                                                                  |
      |1,111,000    |One million one hundred eleven thousand                                                                                               |
      |1,111,100    |One million one hundred eleven thousand one hundred                                                                                   |
      |1,111,110    |One million one hundred eleven thousand one hundred ten                                                                               |
      |1,111,111    |One million one hundred eleven thousand one hundred eleven                                                                            |
      |20,000,000   |Twenty million                                                                                                                        |
      |22,000,000   |Twenty two million                                                                                                                    |
      |22,200,000   |Twenty two million two hundred thousand                                                                                               |
      |22,220,000   |Twenty two million two hundred twenty thousand                                                                                        |
      |22,222,000   |Twenty two million two hundred twenty two thousand                                                                                    |
      |22,222,200   |Twenty two million two hundred twenty two thousand two hundred                                                                        |
      |22,222,220   |Twenty two million two hundred twenty two thousand two hundred twenty                                                                 |
      |22,222,222   |Twenty two million two hundred twenty two thousand two hundred twenty two                                                             |
      |300,000,000  |Three hundred million                                                                                                                 |
      |330,000,000  |Three hundred thirty million                                                                                                          |
      |333,000,000  |Three hundred thirty three million                                                                                                    |
      |333,300,000  |Three hundred thirty three million three hundred thousand                                                                             |
      |333,330,000  |Three hundred thirty three million three hundred thirty thousand                                                                      |
      |333,333,000  |Three hundred thirty three million three hundred thirty three thousand                                                                |
      |333,333,300  |Three hundred thirty three million three hundred thirty three thousand three hundred                                                  |
      |333,333,330  |Three hundred thirty three million three hundred thirty three thousand three hundred thirty                                           |
      |333,333,333  |Three hundred thirty three million three hundred thirty three thousand three hundred thirty three                                     |
      |4,000,000,000|Four billion                                                                                                                          |
      |4,400,000,000|Four billion four hundred million                                                                                                     |
      |4,440,000,000|Four billion four hundred forty million                                                                                               |
      |4,444,000,000|Four billion four hundred forty four million                                                                                          |
      |4,444,400,000|Four billion four hundred forty four million four hundred thousand                                                                    |
      |4,444,440,000|Four billion four hundred forty four million four hundred forty thousand                                                              |
      |4,444,444,000|Four billion four hundred forty four million four hundred forty four thousand                                                         |
      |4,444,444,400|Four billion four hundred forty four million four hundred forty four thousand four hundred                                            |
      |4,444,444,440|Four billion four hundred forty four million four hundred forty four thousand four hundred forty                                      |
      |4,444,444,444|Four billion four hundred forty four million four hundred forty four thousand four hundred forty four                                 |
      |5000000000000|Five trillion                                                                                                                         |
      |5500000000000|Five trillion five hundred billion                                                                                                    |
      |5550000000000|Five trillion five hundred fifty billion                                                                                              |
      |5555000000000|Five trillion five hundred fifty five billion                                                                                         |
      |5555500000000|Five trillion five hundred fifty five billion five hundred million                                                                    |
      |5555550000000|Five trillion five hundred fifty five billion five hundred fifty million                                                              |
      |5555555000000|Five trillion five hundred fifty five billion five hundred fifty five million                                                         |
      |5555555500000|Five trillion five hundred fifty five billion five hundred fifty five million five hundred thousand                                   |
      |5555555550000|Five trillion five hundred fifty five billion five hundred fifty five million five hundred fifty thousand                             |
      |5555555555000|Five trillion five hundred fifty five billion five hundred fifty five million five hundred fifty five thousand                        |
      |5555555555500|Five trillion five hundred fifty five billion five hundred fifty five million five hundred fifty five thousand five hundred           |
      |5555555555550|Five trillion five hundred fifty five billion five hundred fifty five million five hundred fifty five thousand five hundred fifty     |
      |5555555555555|Five trillion five hundred fifty five billion five hundred fifty five million five hundred fifty five thousand five hundred fifty five|
      |2.25         |Two and 25/100                                                                                                                        |
      |3.1          |Three and 10/100                                                                                                                      |

  Scenario: Stop server
    Then I stop the server