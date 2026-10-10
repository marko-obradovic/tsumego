# Tsumego

Game: The entire game
Gamenode: A single move in the game node tree


# Things to implement:
    create test suite
    capture stones
    - Check if legal then capture
    - Ko - prevent infinite loop
    - you cannot place a stone in the middle of 4 stones - call it IsSuicideMove()
        - should be able to capture on edges
        - you can if the stone has no liberties (then its a capture)

# Things to test for:
- When stone on the edge, does it not count liberties outside the range of the board?
- Does it prevent you from putting a stone on a stone that's currently on the board?
