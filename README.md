# Genshindle Solver

A console based solver for the Genshindle game in Miliastra Wonderland (Genshin Impact).

Pretty much just a wordle statistical solver except for Genshindle.

Made in about 2.5 hours because I got bored and didn't want to study for my compiler finals.

Has options to use either the Entropy based solver or Expected Value based solver. Google is your friend to find out about what these do differently.

For the normal mode (with 5 guesses), this is guaranteed to solve within the 5 turns and the current character list (Genshin Luna VI) and there is no difference between the Entropy based solver or Expected Value based solver (or at least the number of guesses required across all characters were the exact same).

This is a list of the number of guesses and number of characters that require that many guesses. This is the same regardless of which solver mode you use.
* 1: 1
* 2: 6
* 3: 55
* 4: 51
* 5: 0


For hard mode, once it gets to 3 random initial guesses, there's about a 6.53% (Expected Value), and 6.39% (Entropy) chance that it will require more than 2 more guesses to solve, therefore failing to solve within the number of guesses. This is tested by doing simulating 10 million iterations of the game with 3 random initial guesses on a randomly picked character.

Btw Mika is the statistically optimal first guess in Genshindle, which makes sense. Why this logically makes sense besides the statistical analysis is left as an exercise to the reader.

