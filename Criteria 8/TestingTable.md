# Testing Table

| Test Case ID | Description                                     | Expected Result                     | Actual Result                                                                   | Status |
|--------------|-------------------------------------------------|-------------------------------------|---------------------------------------------------------------------------------|--------|
| TC001        | Verify that the application starts successfully | Application launches without errors | Application launched successfully                                               | Pass   |
| TC002        | Check if the main menu is displayed             | Main menu appears with options      | Main menu displayed correctly                                                   | Pass   |
| TC003        | Test player A's turn with valid die placement   | Die placed in the grid              | Die placed successfully                                                         | Pass   |
| TC004        | Test player B's turn with valid die placement   | Die placed in the grid              | Die placed successfully                                                         | Pass   |
| TC005        | Verify score calculation after placement        | Score updated correctly             | Score calculated correctly                                                      | Pass   |
| TC006        | Test cancellation mechanic                      | Opponent's dice removed correctly   | Dice cancelled successfully                                                     | Pass   |
| TC007        | Check game end condition when grid is full      | Game ends and winner is declared    | Game ended correctly                                                            | Pass   |
| TC008        | Test AI level 1 decision-making                 | AI selects a valid column           | AI made a valid move                                                            | Pass   |
| TC009        | Test AI level 2 decision-making                 | AI selects the shortest column      | AI made a valid move                                                            | Pass   |
| TC010        | Verify UI Layout on different resolutions       | UI displays correctly               | UI displayed incorrectly under certain resolutions (user fix - change UI scale) | Fail   |
| TC011        | Test button widget functionality                | Button responds to clicks           | Button click registered correctly                                               | Pass   |
| TC012        | Check sound effects on button click             | Sound plays on click                | Sound played successfully                                                       | Pass   |
| TC013        | Test game reset functionality                   | Game resets to initial state        | Game reset successfully                                                         | Pass   |
| TC014        | Invalid Inputs are blocked                      | Invalid input ignored               | Invalid input ignored                                                           | Pass   |

