# Farkle
This is a test driven development c# project. The aim of this project is to create the dice game Farkle.

## Frameworks
XUnit for Unit-testing.
Moq 4.10.0 for Mocking.
Visual Studio Enterprise for Code Coverage.

## Farkle rules
Object of the game: Score 10 000 points.
Equipment: Six 6-sided dices.
Number of players: 2-8.

### Assumptions
##### User wants the highest possible score of the saved dice combination.
##### User wants the highest possible score from rolled dice combination.
##### User can roll max 2 times.

### How to play Farkle (https://www.dicegamedepot.com/farkle-rules/), with some modifications.
One player is chosen to begin and play moves clockwise around the table. Each player in turn rolls all six dice and checks to see if they have rolled any scoring dice or combinations. (See Scoring below.) Any dice that score may be set aside and then the player may choose to roll all the remaining dice. The player must set aside at least one scoring die of their choice if possible but is not required to set aside all scoring dice.

For example, if a player rolled 1-2-2-5-5-6 on their turn, they could set aside the 1 and the two 5's for scoring, or they could choose to set aside only the 1. Any scoring dice that are not set aside may be rerolled along with the non-scoring dice.

If all six dice have been set aside for scoring (known as having “hot dice”), the player can choose to roll all six dice again and continue adding to their accumulated score or they can bank their points, end their turn, and pass the dice to the next player.

A player’s turn continues until either they decide to stop and score their accumulated points or until they fail to roll any scoring dice on a throw. "Each player have a maximum of 2 rolls. /Modified rule". 

If a player scores no points on a roll, this is known as a Farkle. The player may continue to roll the dices if it is the first roll. But, if it is the second roll, then all of points gained in that turn are lost.

At the end of a player’s turn, any points they have scored are written down and the dice are passed to the next player.

#### SCORING
1	100 points
5	50 points
Three 1's	1,000 points
Three 2's	200 points
Three 3's	300 points
Three 4's	400 points
Three 5's	500 points
Three 6's	600 points
1-2-3-4-5-6 	3000 points
3 Pairs	1500 points (including 4-of-a-kind and a pair)
6 of a kind	3000 points (Farkle Score Variation)

Note that scoring combinations only count when made with a single throw. (Example: If a player rolls a 1 and sets it aside and then rolls two 1’s on their next throw, they only score 300 points, not 1000.)

Sometimes a single roll will provide multiple ways to score. For example, a player rolling 1-2-4-5-5-5 could score one of the following:

100 points for the 1
150 points for the 1 and a 5
500 points for the three 5's
600 points for the 1 and the three 5's

The highest possible score will automatically be used, in this case 600 points.

#### WINNING 
The first player to score a total of 10,000 or more points wins, provided that no other players with a remaining turn can exceed that score.

