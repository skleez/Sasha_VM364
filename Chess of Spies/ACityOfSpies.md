#A City of Spies

#Rules

In "A City of Spies," 3 - 6 players will take turns moving and capturing pieces, with the main goal being to have a member of your team as the last piece standing.

The game takes place in rounds, and each round ends when there is only one player remaining.
This player will win the round, and earn 2 points for their team. 

The game ends when a team reaches 5 points.

###Set Up

#####Teams

Players start by seperating and choosing team cards. These Read: **B** or **W**

If you are playing with an odd number of people, the white team will have an extra player. 
>EX: 5 players - 3 W and 2 B'

Do not reveal your team!

#####Pieces
After teams have been assigned, all players must set up the board together so that they agree on placement. 
>**Remember to signify the building in the middle 8 squares of the board (non-possible spaces) (c4,c5,d3,d6,e3,e6,f4,f5).**

The pieces that must be placed on the board are:

**2 Knights**

**2 Bishops**

**2 Rooks**

**4 Pawns**

**1 Queen**

>(Be sure to signify which piece in each pair is #1, or #2 (or 3, or 4 for pawns))



After the pieces have been placed, each player must choose a **Life-Piece** by randomly picking from the pile of **Piece Cards**.

If this **Life-Piece** gets captured at any point during the game, that player is out for the rest of the round.

Players should remember their **Life-Piece**, and then return all the Piece Cards back to the pile.

After every player has recieved a team and a **Life-Piece**, they must draw another card from the **Piece Card** pile. 
This second Piece Card is the player's **Target-Piece**, and will stay in the player's possession until the round ends. 

If a player's **Target-Piece** is captured during their turn, they *may* vocally recognize that their **Target-Piece** was captured, tell everyone which team they are playing for, and then add one point to their team's score.

If a player's **Target-Piece** is captured on someone else's turn, nothing happens, and that player will be unable to earn an extra point for their team that round.

###Round Start

Once every player has a **Life-Piece** and a **Target-Piece**, a new turn order needs to be established.

Each player will roll a die. Turns will be ordered starting with the player with the largest roll, and ending with the smallest.

Once every player is aware of the turn order, the first player's turn can begin.

###Turns

When a player's turn starts, they have the option to move any piece to any available square.

Pieces move extactly as they do in Chess, **however**: 

>there is a *barrier* in the middle of the board that signifies an area where pieces cannot move to.

>Pawns can move one square in *all* directions, up, down, left, and right, and can capture in all diagonal directions.

If the player captures a piece, that piece is removed from the game. If that piece was someones **Life-Piece**, they must vocally recognize it, and that they are out for the round.

Once a player moves a piece, and all subsequent questions are answered, the turn passes to the next player in order.

Once all active players have played one turn, they must all re-roll the die and determine a new order for the next set of turns.

Continue these steps until there is only one active player remaining, that is when the round is over.

###Round End

When the round is over, the last active player must vocally recognize which team they are on, and then add two points to that team's total.

When a team reaches 5 points, the game is over, and that team wins.

If neither team has reached 5 points, restart the game, but just after players draw teams:

>1. Reset the board with correct pieces.
>2. Have all players agree on the placement. 
>3. Redraw **Life-Piece** cards
>4. Redraw **Target-Piece** cards 
>5. Begin a new round by rolling for turn order.




#Playtesting.. A Process

My first idea for this project was a combination of two questions:

>What would chess be like from the point of view of the piece?

and

>In chess the player know exactly which piece is on which side, but what if they didn't?

I built a quick example of what I though a 3rd person-controllable chess piece could be like in a 3D space, but it didn't have any explicit goals that would be engaging or "fun".
My concept clearly didn't have those pillars, and I needed to go back and review the most basic mechanics of the game, and then determine how effective they actually were.

Once I layed out all of the main contstraints of my idea, I could begin to examine all of my non-constraining mechanics. This seemed straight forward, but it required first finding the most basic element of the non-fun, non-constraining mechanic, and then constructing more complex rules on top of them, but only after those more basic mechanics could be described as "fun". 

I became very focused on the concept of the "point of view of the piece," but in playtests the game was not interesting, and didn't yield the strategies I was expecting, or wanting, to see in players. This lead to multiple itterations of they game where the player's options for control were different. Sometimes each player only could control their **Life-Piece**, and every piece on the board moved at the same time (requiring an "AI" method for non-player pieces), and sometimes the players moved their piece twice, but kept the move secret from all other players. All of these playtests were interesting, but the mechanics introduced were often not built upon eachother, and that made it easy for them to be unballanced, un-engaging, or even un-"fun".

Ultimately, I had to review my initial questions, and come up with more basic ones:

>What if there was only one color?
>What is the mission of the individual chess piece?
>How important is the team to chess being multiplayer?

These all lead to the key question of my most recent itteration:

>What if there was only one color, but players could control any piece, and players could capture any piece, have an important **Life-Piece** on the board, but not know which player is on which team?

This lead to a playtest where there was only one color, players chose a **Life-Piece**, players had one move per turn, and last player alive wins.
Finally this playtest was simple enough to yeild very specific results about only a few mechanics. There was strategy even between two players!
Now, could this concept translate to multiple players, but still with only two teams? I did a playtest with three players, and found out that the ultimate, last-piece standing was a good goal, but as an indiviudal on a team, the playtesters didn't feel they had a clear goal in the first few rounds of the game.
Because there weren't other major criticisms with this playtest, I added the new **Target-Piece** mechanic to try and give players a second goal they could persue for their team, but as an individual. I am not sure that this **Target-Piece** will be make the early turns more engaiging, but I am hoping that it will, especially when playing with more players.

This process was difficult, and even though it is not close to over, the strategy of adding mechainics on top of proven working ones (itterative), was very effective at sifting out the stong mechanics from the weak ones. Despite this efficacy, however, I also felt that the itterative process very easily abandoned ideas and concepts, rarely attempting to integrate them, and that a larger-picture (design-document) strategy can be very important to laying out more-engaging and "fun" constraints for your itterations to hold on to. 





