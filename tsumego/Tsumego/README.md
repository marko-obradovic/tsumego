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
    - AI Assistant (you have a chat window, and you can ask it questions that it will explain through actually placing pieces on the board)``
        - Example:
            User: I don't understand why this variation works. Run me through the fundamentals of it by giving me a few example. One simple, one slightly harder, then with that same context, apply it to this exercise and explain it.
            AI:
                1. Got it, let's run through a simpler example.
                2. Resets the board
                3. Places a stone - "Let's say we start at ..."
                4. Pause (wait for user to read and click to continue)
                5. Places another stone - "The idea is ..."
                6. (at each point you have a box you can branch the conversation out of so that you can ask about that specific move, but you retain the main branch where you can continue the conversation)
                7. etc.

# Things to test for:
- When stone on the edge, does it not count liberties outside the range of the board?
- Does it prevent you from putting a stone on a stone that's currently on the board?
