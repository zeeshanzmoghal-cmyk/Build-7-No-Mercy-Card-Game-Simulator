using static System.Net.Mime.MediaTypeNames;

namespace uno
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            try
            {
                //Boot up the game by initializing the cards and the game state
                initializeCards();
                initializeGame();

            }
            catch
            {
                //If anything goes wrong during initialization, show an error message and reset the game
                errorHandler("An error occurred while initializing the game. Please try again.");
            }

        }


        //Initializing global variables
        string[] cardsP1 = new string[25];
        string[] cardsP2 = new string[25];
        string[] cardsP3 = new string[25];
        string[] cardsP4 = new string[25];
        int cardCountP1 = 0;
        int cardCountP2 = 0;
        int cardCountP3 = 0;
        int cardCountP4 = 0;
        string[] zero = new string[4] { "0R", "0G", "0B", "0Y" };
        string[] one = new string[4] { "1R", "1G", "1B", "1Y" };
        string[] two = new string[4] { "2R", "2G", "2B", "2Y" };
        string[] three = new string[4] { "3R", "3G", "3B", "3Y" };
        string[] four = new string[4] { "4R", "4G", "4B", "4Y" };
        string[] five = new string[4] { "5R", "5G", "5B", "5Y" };
        string[] six = new string[4] { "6R", "6G", "6B", "6Y" };
        string[] seven = new string[4] { "7R", "7G", "7B", "7Y" };
        string[] eight = new string[4] { "8R", "8G", "8B", "8Y" };
        string[] nine = new string[4] { "9R", "9G", "9B", "9Y" };
        string[] skipNext = new string[4] { "SNR", "SNG", "SNB", "SNY" };
        string[] reverse = new string[4] { "RR", "RG", "RB", "RY" };
        string[] pickupTwo = new string[4] { "P2R", "P2G", "P2B", "P2Y" };
        string[] pickupFour = new string[4] { "P4R", "P4G", "P4B", "P4Y" };
        string[] skipAll = new string[4] { "SAR", "SAG", "SAB", "SAY" };
        string[] discard = new string[4] { "DR", "DG", "DB", "DY" };
        string reversePickupFour;
        string pickupSixWild;
        string pickupTenWild;
        string roulletteWild;
        bool p1Lost = false;
        bool p2Lost = false;
        bool p3Lost = false;
        bool p4Lost = false;
        string previousCardPlayed = "";
        Color p1bgColor;
        Color p2bgColor;
        Color p3bgColor;
        Color p4bgColor;


        public void initializeGame()
        {
            //Initialize variables
            string flippedCard;
            flippedCard = pickRandomCard();

            while (flippedCard ==  null) // Protective measure to stop a null item being the previous card on the deck
            {
                flippedCard = pickRandomCard();
            }

            btnReset.Enabled = false; // Disable the reset button to prevent players from resetting the game during play 
            btnPickup.Enabled = true; // Enable the pickup button to allow players to pick up cards during their turn

            // Reset the lost status for each player to false to prepare for a new game
            p1Lost = false;
            p2Lost = false;
            p3Lost = false;
            p4Lost = false;

            // Reset the card counts for each player to 0 to prepare for a new game
            cardCountP1 = 0;
            cardCountP2 = 0;
            cardCountP3 = 0;
            cardCountP4 = 0;

            // Set the images for the card picture boxes to the back of a card to indicate that the cards are face down at the start of the game
            pbCardsP1.Image = Properties.Resources.any_card_rear;
            pbCardsP2.Image = Properties.Resources.any_card_rear;
            pbCardsP3.Image = Properties.Resources.any_card_rear;
            pbCardsP4.Image = Properties.Resources.any_card_rear;

            //Clear the combo boxes for each player to prepare for a new game
            cbP1PlayableCard.Items.Clear();
            cbP2PlayableCard.Items.Clear();
            cbP3PlayableCard.Items.Clear();
            cbP4PlayableCard.Items.Clear();

            //Reset the text of the combo boxes to an empty string
            cbP1PlayableCard.Text = "";
            cbP2PlayableCard.Text = "";
            cbP3PlayableCard.Text = "";
            cbP4PlayableCard.Text = "";

            //Enable the combo boxes and buttons for each player to allow them to play the game
            cbP1PlayableCard.Enabled = true;
            cbP2PlayableCard.Enabled = true;
            cbP3PlayableCard.Enabled = true;
            cbP4PlayableCard.Enabled = true;

            btnP1Play.Enabled = true;
            btnP2Play.Enabled = true;
            btnP3Play.Enabled = true;
            btnP4Play.Enabled = true;

            btnP1UNO.Enabled = true;
            btnP2UNO.Enabled = true;
            btnP3UNO.Enabled = true;
            btnP4UNO.Enabled = true;

            // Reset the reverse direction flag to false to prepare for a new game
            reverseDirection = false;

            //Give each player 7 random cards and add them to their hand
            for (int i = 0; i < 7; i++)
            {
                cardsP1[i] = pickRandomCard();
                cardCountP1++;
                cbP1PlayableCard.Items.Add(cardsP1[i]);
            }
            for (int j = 0; j < 7; j++)
            {
                cardsP2[j] = pickRandomCard();
                cardCountP2++;
                cbP2PlayableCard.Items.Add(cardsP2[j]);
            }
            for (int k = 0; k < 7; k++)
            {
                cardsP3[k] = pickRandomCard();
                cardCountP3++;
                cbP3PlayableCard.Items.Add(cardsP3[k]);
            }
            for (int l = 0; l < 7; l++)
            {
                cardsP4[l] = pickRandomCard();
                cardCountP4++;
                cbP4PlayableCard.Items.Add(cardsP4[l]);
            }

            setDiscardDeck(flippedCard);

            // Display a message box to indicate which player will start the game
            MessageBox.Show(randomStartTurn(), "Game Start", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        bool setStartDeck; // Flag to indicate whether the discard deck has been set or not

        public void setDiscardDeck(string discardCard)
        {
            while (discardCard == null) // If the discard card is null, set the flag to false to indicate that the discard deck has not been set
            {
                setStartDeck = false;
            }

            if (setStartDeck == false) // If the discard card is not null, set the flag to true to indicate that the discard deck has been set
            {
                setStartDeck = true;
            }

            if (setStartDeck == true) // If the discard deck has been set, display the flipped card on the picture box and set the previous card played to the flipped card
            {
                //Flip a random card to start the game and display it on the picture box
                switch (discardCard)
                {
                    //Check the value of the flipped card and display the corresponding image in the picture box
                    case string c when c.Contains("Zero"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_0;
                                break;

                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_0;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_0;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_0;
                                break;

                        }
                        break;
                    case string c when c.Contains("One"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_1;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_1;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_1;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_1;
                                break;
                        }
                        break;
                    case string c when c.Contains("Two"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_2;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_2;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_2;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_2;
                                break;
                        }
                        break;
                    case string c when c.Contains("Three"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_3;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_3;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_3;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_3;
                                break;
                        }
                        break;
                    case string c when c.Contains("Four"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_4;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_4;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_4;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_4;
                                break;
                        }
                        break;
                    case string c when c.Contains("Five"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_5;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_5;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_5;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_5;
                                break;
                        }
                        break;
                    case string c when c.Contains("Six"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_6;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_6;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_6;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_6;
                                break;
                        }
                        break;
                    case string c when c.Contains("Seven"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_7;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_7;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_7;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_7;
                                break;
                        }
                        break;
                    case string c when c.Contains("Eight"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_8;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_8;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_8;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_8;
                                break;
                        }
                        break;
                    case string c when c.Contains("Nine"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_9;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_9;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_9;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_9;
                                break;
                        }
                        break;
                    case string c when c.Contains("Skip Next"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_skip_next;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_skip_next;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_skip_next;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_skip_next;
                                break;
                        }
                        break;
                    case string c when c.Contains("Reverse"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_reverse;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_reverse;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_reverse;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_reverse;
                                break;
                        }
                        break;
                    case string c when c.Contains("Pickup 2"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_pickup_2;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_pickup_2;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_pickup_2;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_pickup_2;
                                break;
                        }
                        break;
                    case string c when c.Contains("Pickup 4"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_pickup_4;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_pickup_4;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_pickup_4;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_pickup_4;
                                break;
                            case string card when card.Contains("Switch"):
                                break;
                        }
                        break;
                    case string c when c.Contains("Discard"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_discard;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_discard;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_discard;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_discard;
                                break;
                        }
                        break;
                    case string c when c.Contains("Skip All"):
                        switch (discardCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsPlayed.Image = Properties.Resources.red_skip_all;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsPlayed.Image = Properties.Resources.green_skip_all;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsPlayed.Image = Properties.Resources.blue_skip_all;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsPlayed.Image = Properties.Resources.yellow_skip_all;
                                break;
                        }
                        break;
                    //Check for wild cards and display the corresponding image in the picture box
                    case string c when c.Contains("Pickup 6"):
                        deckSetterAfterWildCard(wildCardChosenColor); // Set the discard deck to the color chosen by the player for the Pickup 6 wild card
                        return; // Exit the function after setting the discard deck for the Pickup 6 card
                    case string c when c.Contains("Pickup 10"):
                        deckSetterAfterWildCard(wildCardChosenColor); // Set the discard deck to the color chosen by the player for the Pickup 10 wild card
                        return;     // Exit the function after setting the discard deck for the Pickup 10 card
                    case string c when c.Contains("Roulette"):
                        deckSetterAfterWildCard(wildCardChosenColor); // Set the discard deck to the color chosen by the player for the Roulette wild card
                        return; // Exit the function after setting the discard deck for the Roulette card

                }

                previousCardPlayed = discardCard; // Set the previous card played to the flipped card to allow for color matching logic in subsequent turns

            }
        }

        public void initializeCards()
        {
            //Initialize the card data for all the cards in the game
            zero[0] = "Zero (Red)";
            zero[1] = "Zero (Green)";
            zero[2] = "Zero (Blue)";
            zero[3] = "Zero (Yellow)";
            one[0] = "One (Red)";
            one[1] = "One (Green)";
            one[2] = "One (Blue)";
            one[3] = "One (Yellow)";
            two[0] = "Two (Red)";
            two[1] = "Two (Green)";
            two[2] = "Two (Blue)";
            two[3] = "Two (Yellow)";
            three[0] = "Three (Red)";
            three[1] = "Three (Green)";
            three[2] = "Three (Blue)";
            three[3] = "Three (Yellow)";
            four[0] = "Four (Red)";
            four[1] = "Four (Green)";
            four[2] = "Four (Blue)";
            four[3] = "Four (Yellow)";
            five[0] = "Five (Red)";
            five[1] = "Five (Green)";
            five[2] = "Five (Blue)";
            five[3] = "Five (Yellow)";
            six[0] = "Six (Red)";
            six[1] = "Six (Green)";
            six[2] = "Six (Blue)";
            six[3] = "Six (Yellow)";
            seven[0] = "Seven (Red)";
            seven[1] = "Seven (Green)";
            seven[2] = "Seven (Blue)";
            seven[3] = "Seven (Yellow)";
            eight[0] = "Eight (Red)";
            eight[1] = "Eight (Green)";
            eight[2] = "Eight (Blue)";
            eight[3] = "Eight (Yellow)";
            nine[0] = "Nine (Red)";
            nine[1] = "Nine (Green)";
            nine[2] = "Nine (Blue)";
            nine[3] = "Nine (Yellow)";
            skipNext[0] = "Skip Next (Red)";
            skipNext[1] = "Skip Next (Green)";
            skipNext[2] = "Skip Next (Blue)";
            skipNext[3] = "Skip Next (Yellow)";
            reverse[0] = "Reverse (Red)";
            reverse[1] = "Reverse (Green)";
            reverse[2] = "Reverse (Blue)";
            reverse[3] = "Reverse (Yellow)";
            pickupFour[0] = "Pickup 4 (Red)";
            pickupFour[1] = "Pickup 4 (Green)";
            pickupFour[2] = "Pickup 4 (Blue)";
            pickupFour[3] = "Pickup 4 (Yellow)";
            pickupTwo[0] = "Pickup 2 (Red)";
            pickupTwo[1] = "Pickup 2 (Green)";
            pickupTwo[2] = "Pickup 2 (Blue)";
            pickupTwo[3] = "Pickup 2 (Yellow)";
            discard[0] = "Discard (Red)";
            discard[1] = "Discard (Green)";
            discard[2] = "Discard (Blue)";
            discard[3] = "Discard (Yellow)";
            skipAll[0] = "Skip All (Red)";
            skipAll[1] = "Skip All (Green)";
            skipAll[2] = "Skip All (Blue)";
            skipAll[3] = "Skip All (Yellow)";
            skipNext[0] = "Skip Next (Red)";
            skipNext[1] = "Skip Next (Green)";
            skipNext[2] = "Skip Next (Blue)";
            skipNext[3] = "Skip Next (Yellow)";
            pickupSixWild = "Pickup 6";
            pickupTenWild = "Pickup 10";
            roulletteWild = "Roulette";
            reversePickupFour = "Pickup 4 (Switch)";
        }

        public string pickRandomCard()
        {
            //Initialize a random number generator and pick a random card from the deck
            Random rand = new Random();
            int cardIndex = rand.Next(0, 20);
            int colourIndex = rand.Next(0, 4);
            string card = "";

            //Use the random indices to select a card from the corresponding arrays and return it
            switch (cardIndex)
            {
                case 0:
                    card = zero[colourIndex];
                    break;
                case 1:
                    card = one[colourIndex];
                    break;
                case 2:
                    card = two[colourIndex];
                    break;
                case 3:
                    card = three[colourIndex];
                    break;
                case 4:
                    card = four[colourIndex];
                    break;
                case 5:
                    card = five[colourIndex];
                    break;
                case 6:
                    card = six[colourIndex];
                    break;
                case 7:
                    card = seven[colourIndex];
                    break;
                case 8:
                    card = eight[colourIndex];
                    break;
                case 9:
                    card = nine[colourIndex];
                    break;
                case 10:
                    card = skipNext[colourIndex];
                    break;
                case 11:
                    card = reverse[colourIndex];
                    break;
                case 12:
                    card = pickupTwo[colourIndex];
                    break;
                case 13:
                    card = pickupFour[colourIndex];
                    break;
                case 14:
                    card = discard[colourIndex];
                    break;
                case 15:
                    card = skipAll[colourIndex];
                    break;
                case 16:
                    card = pickupSixWild;
                    break;
                case 17:
                    card = pickupTenWild;
                    break;
                case 18:
                    card = roulletteWild;
                    break;
                case 19:
                    card = reversePickupFour;
                    break;
            }

            // Return the randomly selected card
            return card;
        }

        public bool unoEvent(int cardCount)
        {
            if (cardCount > 2)
            {
                //If the player has more than 2 cards, they cannot call UNO and the function returns false
                return false;

            }
            return true;
        }
        // helper: returns "Red"/"Green"/"Blue"/"Yellow" or null
        private string GetCardColor(string card)
        {
            if (string.IsNullOrEmpty(card))
            {
                return null;
            }
            if (card.Contains("Red"))
            {
                return "Red";
            }
            if (card.Contains("Green"))
            {
                return "Green";
            }
            if (card.Contains("Blue"))
            {
                return "Blue";
            }
            if (card.Contains("Yellow"))
            {
                return "Yellow";
            }
            return null;
        }

        public bool sameColorCheck(string cardPlayed)
        {
            //Check if the card played is the same color as the previous card played and return true if it is, otherwise return false
            // If cardPlayed is a wild-type card, allow it (wilds are always playable)
            if (cardPlayed.Contains("Pickup 6") || cardPlayed.Contains("Pickup 10")
                || cardPlayed.Contains("Roulette") || cardPlayed.Contains("Switch"))
            {
                return true;
            }

            // Compare explicit colours parsed from strings
            string playedColor = GetCardColor(cardPlayed);
            string prevColor = GetCardColor(previousCardPlayed);

            // If previous was a wild and was converted into a colored placeholder (your handler sets previousCardPlayed to "One (Red)" etc),
            // prevColor will be set and the comparison below will ensure only matching colour is allowed.
            if (!string.IsNullOrEmpty(playedColor) && !string.IsNullOrEmpty(prevColor))
            {
                return playedColor == prevColor;
            }

            // fallback: no colour match
            return false;
        }

        public bool sameValueCheck(string cardPlayed)
        {
            //Check if the card played is the same value as the previous card played and return true if it is, otherwise return false
            string playedValue = cardPlayed.Split(' ')[0]; // Get the value of the card played (e.g. "One", "Two", "Three", etc.)
            string prevValue = previousCardPlayed.Split(' ')[0]; // Get the value of the previous card played
            if (playedValue == prevValue)
            {
                // If the values match, return true
                return true;
            }
            else
            {
                // If the values do not match, return false
                return false;
            }
        }

        public bool sameSpecialColoredCardCheck(string cardPlayed)
        {
            //Check if the card played is the same special colored card as the previous card played and return true if it is, otherwise return false
            string playedSpecial = cardPlayed.Split(' ')[0]; // Get the special type of the card played (e.g. "Skip", "Reverse", "Pickup", etc.)
            string prevSpecial = previousCardPlayed.Split(' ')[0]; // Get the special type of the previous card played
            if (playedSpecial == prevSpecial)
            {
                // If the special types match, return true
                return true;
            }
            else
            {
                // If the special types do not match, return false
                return false;
            }
        }

        public void skipAllEvent(string cardId)
        {
            //This function is called when a player plays a Skip All card, it checks which player played the card and skips the turns of the other players accordingly by calling the nextTurnEvent function with the correct parameters
            if (cardId.Contains("Skip All"))
            {
                // Display a message box to indicate that all other players have been skipped
                MessageBox.Show("All other players have been skipped!", "Skip All", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Do nothing if the card played is not a Skip All card
            else
            {
                return;
            }
        }

        public void skipNextEvent(string currentPlayer, string cardId)
        {
            //This function is called when a player plays a Skip Next card, it checks which player played the card and skips the turn of the next player accordingly by calling the nextTurnEvent function with the correct parameters
            if (cardId.Contains("Skip Next"))
            {
                if (reverseDirection == false) // If the direction of play is normal (not reversed), skip the next player in the normal order
                {
                    switch (currentPlayer)
                    {
                        case string c when c.Contains("Player 1"):
                            nextTurnEvent(getPlayerName(gbP2.Text));
                            break;
                        case string c when c.Contains("Player 2"):
                            nextTurnEvent(getPlayerName(gbP3.Text));
                            break;
                        case string c when c.Contains("Player 3"):
                            nextTurnEvent(getPlayerName(gbP4.Text));
                            break;
                        case string c when c.Contains("Player 4"):
                            nextTurnEvent(getPlayerName(gbP1.Text));
                            break;
                    }
                }
                else // If the direction of play is reversed, skip the next player in the reverse order
                {
                    switch (currentPlayer)
                    {
                        case string c when c.Contains("Player 1"):
                            nextTurnEvent(getPlayerName(gbP4.Text));
                            break;
                        case string c when c.Contains("Player 2"):
                            nextTurnEvent(getPlayerName(gbP1.Text));
                            break;
                        case string c when c.Contains("Player 3"):
                            nextTurnEvent(getPlayerName(gbP2.Text));
                            break;
                        case string c when c.Contains("Player 4"):
                            nextTurnEvent(getPlayerName(gbP3.Text));
                            break;
                    }
                }
                // Display a message box to indicate that the next player has been skipped
                MessageBox.Show("The next player has been skipped!", "Skip Next", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Do nothing if the card played is not a Skip Next card
            else
            {
                return;
            }
        }

        public void numberCardEvent(string currentPlayer, string cardId)
        {
            //This function is called when a player plays a number card, it checks which player played the card and updates the game state accordingly by calling the nextTurnEvent function with the correct parameters
            if (cardId.Contains("One") || cardId.Contains("Two") || cardId.Contains("Three") || cardId.Contains("Four") || cardId.Contains("Five") || cardId.Contains("Six") || cardId.Contains("Eight") || cardId.Contains("Nine"))
            {
                switch (currentPlayer)
                {
                    case string c when c.Contains("Player 1"):
                        nextTurnEvent(getPlayerName(currentPlayer));
                        break;
                    case string c when c.Contains("Player 2"):
                        nextTurnEvent(getPlayerName(currentPlayer));
                        break;
                    case string c when c.Contains("Player 3"):
                        nextTurnEvent(getPlayerName(currentPlayer));
                        break;
                    case string c when c.Contains("Player 4"):
                        nextTurnEvent(getPlayerName(currentPlayer));
                        break;
                }
            }
            // Do nothing if the card played is not a number card
            else
            {
                return;
            }
        }

        bool reverseDirection = false;

        public void reverseCardEvent(string currentPlayer, string cardId)
        {
            //This function is called when a player plays a Reverse card, it checks which player played the card and updates the game state accordingly by calling the nextTurnEvent function with the correct parameters to reverse the order of play
            if (cardId.Contains("Reverse") || cardId.Contains("Pickup 4 (Switch)"))
            {
                // Display a message box to indicate that the direction of play has been reversed
                MessageBox.Show("The direction of play has been reversed!", "Reverse Direction", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reverseDirection = !reverseDirection;
                nextTurnEvent(getPlayerName(currentPlayer));
            }
            // Do nothing if the card played is not a Reverse card
            else
            {
                return;
            }
        }
        public void discardCardEvent(string currentPlayer, string nextPlayer, string cardId)
        {
            // This function is called when a player plays a Discard card, it checks which player played the card and removes all cards of the same color as the Discard card from that player's hand by shifting the remaining cards in the array and updating the card count and combo box items accordingly
            if (cardId.Contains("Discard"))
            {
                // iterate only until Length-1 so i+1 is always valid when shifting
                for (int i = 0; i < 24; i++)
                {
                    switch (cardId)
                    {
                        // Check the color of the Discard card and remove all cards of that color from the current player's hand by shifting the remaining cards in the array and updating the card count and combo box items accordingly
                        case string c when c.Contains("Red"):
                            if (currentPlayer == "Player 1" && cardsP1[i] != null && cardsP1[i].Contains("Red"))
                            {
                                string removed = cardsP1[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP1[j] = cardsP1[j + 1];
                                }
                                cardsP1[24] = null; // Clear the last slot after shifting
                                cardCountP1--;
                                cbP1PlayableCard.Items.Remove(removed);
                                i--; // re-check this index after shift
                            }
                            // Repeat the same logic for the other players
                            else if (currentPlayer == "Player 2" && cardsP2[i] != null && cardsP2[i].Contains("Red"))
                            {
                                string removed = cardsP2[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP2[j] = cardsP2[j + 1];
                                }
                                cardsP2[24] = null; // Clear the last slot after shifting
                                cardCountP2--;
                                cbP2PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            else if (currentPlayer == "Player 3" && cardsP3[i] != null && cardsP3[i].Contains("Red"))
                            {
                                string removed = cardsP3[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP3[j] = cardsP3[j + 1];
                                }
                                cardsP3[24] = null; // Clear the last slot after shifting
                                cardCountP3--;
                                cbP3PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            else if (currentPlayer == "Player 4" && cardsP4[i] != null && cardsP4[i].Contains("Red"))
                            {
                                string removed = cardsP4[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP4[j] = cardsP4[j + 1];
                                }
                                cardsP4[24] = null; // Clear the last slot after shifting
                                cardCountP4--;
                                cbP4PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            break;
                        // Repeat the same logic for Green, Blue, and Yellow Discard cards
                        case string c when c.Contains("Blue"):
                            if (currentPlayer == "Player 1" && cardsP1[i] != null && cardsP1[i].Contains("Blue"))
                            {
                                string removed = cardsP1[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP1[j] = cardsP1[j + 1];
                                }
                                cardsP1[24] = null; // Clear the last slot after shifting
                                cardCountP1--;
                                cbP1PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            else if (currentPlayer == "Player 2" && cardsP2[i] != null && cardsP2[i].Contains("Blue"))
                            {
                                string removed = cardsP2[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP2[j] = cardsP2[j + 1];
                                }
                                cardsP2[24] = null; // Clear the last slot after shifting
                                cardCountP2--;
                                cbP2PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            else if (currentPlayer == "Player 3" && cardsP3[i] != null && cardsP3[i].Contains("Blue"))
                            {
                                string removed = cardsP3[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP3[j] = cardsP3[j + 1];
                                }
                                cardsP3[24] = null; // Clear the last slot after shifting
                                cardCountP3--;
                                cbP3PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            else if (currentPlayer == "Player 4" && cardsP4[i] != null && cardsP4[i].Contains("Blue"))
                            {
                                string removed = cardsP4[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP4[j] = cardsP4[j + 1];
                                }
                                cardsP4[24] = null; // Clear the last slot after shifting
                                cardCountP4--;
                                cbP4PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            break;
                        case string c when c.Contains("Green"):
                            if (currentPlayer == "Player 1" && cardsP1[i] != null && cardsP1[i].Contains("Green"))
                            {
                                string removed = cardsP1[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP1[j] = cardsP1[j + 1];
                                }
                                cardsP1[24] = null; // Clear the last slot after shifting
                                cardCountP1--;
                                cbP1PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            else if (currentPlayer == "Player 2" && cardsP2[i] != null && cardsP2[i].Contains("Green"))
                            {
                                string removed = cardsP2[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP2[j] = cardsP2[j + 1];
                                }
                                cardsP2[24] = null; // Clear the last slot after shifting
                                cardCountP2--;
                                cbP2PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            else if (currentPlayer == "Player 3" && cardsP3[i] != null && cardsP3[i].Contains("Green"))
                            {
                                string removed = cardsP3[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP3[j] = cardsP3[j + 1];
                                }
                                cardsP3[24] = null; // Clear the last slot after shifting
                                cardCountP3--;
                                cbP3PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            else if (currentPlayer == "Player 4" && cardsP4[i] != null && cardsP4[i].Contains("Green"))
                            {
                                string removed = cardsP4[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP4[j] = cardsP4[j + 1];
                                }
                                cardsP4[24] = null; // Clear the last slot after shifting
                                cardCountP4--;
                                cbP4PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            break;
                        case string c when c.Contains("Yellow"):
                            if (currentPlayer == "Player 1" && cardsP1[i] != null && cardsP1[i].Contains("Yellow"))
                            {
                                string removed = cardsP1[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP1[j] = cardsP1[j + 1];
                                }
                                cardsP1[24] = null; // Clear the last slot after shifting
                                cardCountP1--;
                                cbP1PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            else if (currentPlayer == "Player 2" && cardsP2[i] != null && cardsP2[i].Contains("Yellow"))
                            {
                                string removed = cardsP2[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP2[j] = cardsP2[j + 1];
                                }
                                cardsP2[24] = null; // Clear the last slot after shifting
                                cardCountP2--;
                                cbP2PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            else if (currentPlayer == "Player 3" && cardsP3[i] != null && cardsP3[i].Contains("Yellow"))
                            {
                                string removed = cardsP3[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP3[j] = cardsP3[j + 1];
                                }
                                cardsP3[24] = null; // Clear the last slot after shifting
                                cardCountP3--;
                                cbP3PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            else if (currentPlayer == "Player 4" && cardsP4[i] != null && cardsP4[i].Contains("Yellow"))
                            {
                                string removed = cardsP4[i];
                                for (int j = i; j < 24; j++)
                                {
                                    cardsP4[j] = cardsP4[j + 1];
                                }
                                cardsP4[24] = null; // Clear the last slot after shifting
                                cardCountP4--;
                                cbP4PlayableCard.Items.Remove(removed);
                                i--;
                            }
                            break;
                    }
                }
                // Display a message box to indicate that all cards of the selected color have been discarded and call the nextTurnEvent function with the next player as a parameter to update the game state accordingly
                MessageBox.Show("You have discarded all cards of the selected color.", "Discard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nextTurnEvent(getPlayerName(currentPlayer)); // Call the nextTurnEvent function with the next player as a parameter to update the game state accordingly
            }
            // Do nothing if the card played is not a Discard card
            else
            {
                return;
            }
        }

        string rouletteNewCardID; // Initialize a global variable to store the value roulette card needs to know

        public string getPlayerName(string playerName)
        {
            string currentPlayer = ""; // Set the current player name to the playerName parameter for use in determining the next player based on the current direction of play and returning the corresponding group box text for the next player
            // Check the current direction of play and determine the next player based on the current player name and return the corresponding group box text for the next player
            if (reverseDirection == false) // If the direction of play is normal, determine the next player based on the current player name and return the corresponding group box text for the next player
            {
                if (playerName == "Player 1")
                {
                    currentPlayer = gbP2.Text;
                }
                else if (playerName == "Player 2")
                {
                    currentPlayer = gbP3.Text;
                }
                else if (playerName == "Player 3")
                {
                    currentPlayer = gbP4.Text;
                }
                else if (playerName == "Player 4")
                {
                    currentPlayer = gbP1.Text;
                }
            }
            else // If the direction of play is reversed, determine the next player based on the current player name and return the corresponding group box text for the next player in reverse order
            {
                if (playerName == "Player 1")
                {
                    currentPlayer = gbP4.Text;
                }
                else if (playerName == "Player 2")
                {
                    currentPlayer = gbP1.Text;
                }
                else if (playerName == "Player 3")
                {
                    currentPlayer = gbP2.Text;
                }
                else if (playerName == "Player 4")
                {
                    currentPlayer = gbP3.Text;
                }
            }
            // Return the current player name for use in determining the next player based on the current direction of play and returning the corresponding group box text for the next player
            return currentPlayer;
        }
        // NOW THAT ALL LOGIC ERRORS ARE SOLVED, AND ALL THE GAME FUNCTIONALITY IS DONE, ITS TIME FOR THE VISUAL ASPECT OF THE GAME, WHICH IS THE FINAL STEP OF THE PROJECT.
        public bool addCardToPlayer(string playerName, int cardNumber)
        {
            if (playerName == "Player 1" && p1Lost && !p2Lost && !reverseDirection) // If Player 1 has already lost change the playerName to Player 2 to add cards to the next player in line, otherwise keep the playerName as Player 1 to add cards to their hand
            {
                playerName = "Player 2";
            }
            else if (playerName == "Player 1" && p1Lost && p2Lost && !reverseDirection) // If Player 1 and Player 2 have already lost change the playerName to Player 3 to add cards to the next player in line, otherwise keep the playerName as Player 1 to add cards to their hand
            {
                playerName = "Player 3";

            }
            else if (playerName == "Player 2" && p2Lost && !p3Lost && !reverseDirection) // If Player 2 has already lost change the playerName to Player 3 to add cards to the next player in line, otherwise keep the playerName as Player 2 to add cards to their hand
            {
                playerName = "Player 3";
            }
            else if (playerName == "Player 2" && p2Lost && p3Lost && !reverseDirection) // If Player 2 and Player 3 have already lost change the playerName to Player 4 to add cards to the next player in line, otherwise keep the playerName as Player 2 to add cards to their hand
            {
                playerName = "Player 4";
            }
            else if (playerName == "Player 3" && p3Lost && !p4Lost && !reverseDirection) // If Player 3 has already lost change the playerName to Player 4 to add cards to the next player in line, otherwise keep the playerName as Player 3 to add cards to their hand
            {
                playerName = "Player 4";
            }
            else if (playerName == "Player 3" && p3Lost && p4Lost && !reverseDirection) // If Player 3 and Player 4 have already lost change the playerName to Player 1 to add cards to the next player in line, otherwise keep the playerName as Player 3 to add cards to their hand
            {
                playerName = "Player 1";
            }
            else if (playerName == "Player 4" && p4Lost && !p1Lost && !reverseDirection) // If Player 4 has already lost change the playerName to Player 1 to add cards to the next player in line, otherwise keep the playerName as Player 4 to add cards to their hand
            {
                playerName = "Player 1";
            }
            else if (playerName == "Player 4" && p4Lost && p1Lost && !reverseDirection) // If Player 4 and Player 1 have already lost change the playerName to Player 2 to add cards to the next player in line, otherwise keep the playerName as Player 4 to add cards to their hand
            {
                playerName = "Player 2";
            }
            else if (playerName == "Player 1" && p1Lost && !p4Lost && reverseDirection) // If Player 1 has already lost change the playerName to Player 4 to add cards to the next player in line, otherwise keep the playerName as Player 1 to add cards to their hand
            {
                playerName = "Player 4";
            }
            else if (playerName == "Player 1" && p1Lost && p4Lost && reverseDirection) // If Player 1 and Player 4 have already lost change the playerName to Player 3 to add cards to the next player in line, otherwise keep the playerName as Player 1 to add cards to their hand
            {
                playerName = "Player 3";
            }
            else if (playerName == "Player 2" && p2Lost && !p1Lost && reverseDirection) // If Player 2 has already lost change the playerName to Player 1 to add cards to the next player in line, otherwise keep the playerName as Player 2 to add cards to their hand
            {
                playerName = "Player 1";

            }
            else if (playerName == "Player 2" && p2Lost && p1Lost && reverseDirection) // If Player 2 and Player 1 have already lost change the playerName to Player 4 to add cards to the next player in line, otherwise keep the playerName as Player 2 to add cards to their hand
            {
                playerName = "Player 4";
            }
            else if (playerName == "Player 3" && p3Lost && !p2Lost && reverseDirection) // If Player 3 has already lost change the playerName to Player 2 to add cards to the next player in line, otherwise keep the playerName as Player 3 to add cards to their hand
            {
                playerName = "Player 2";

            }
            else if (playerName == "Player 3" && p3Lost && p2Lost && reverseDirection) // If Player 3 and Player 2 have already lost change the playerName to Player 1 to add cards to the next player in line, otherwise keep the playerName as Player 3 to add cards to their hand
            {
                playerName = "Player 1";
            }
            else if (playerName == "Player 4" && p4Lost && !p3Lost && reverseDirection) // If Player 4 has already lost change the playerName to Player 3 to add cards to the next player in line, otherwise keep the playerName as Player 4 to add cards to their hand
            {
                playerName = "Player 3";

            }
            else if (playerName == "Player 4" && p4Lost && p3Lost && reverseDirection) // If Player 4 and Player 3 have already lost change the playerName to Player 2 to add cards to the next player in line, otherwise keep the playerName as Player 4 to add cards to their hand
            {
                playerName = "Player 2";
            }



            for (int i = 0; i < cardNumber; i++) // Loop through the number of cards to add to the player's hand and pick a random card from the deck, then check which player is being updated and add the new card to their hand by updating the corresponding array, combo box items, and card count accordingly. If a player has already lost, do not add any more cards and return false. If a player reaches the maximum card count of 25, trigger the loseEvent function for that player and return true.
            {
                string newCard = pickRandomCard();
                rouletteNewCardID = newCard;

                if (playerName.Contains("Player 1")) // Check if the player being updated is Player 1 and add the new card to their hand by updating the corresponding array, combo box items, and card count accordingly
                {
                    if (p1Lost) // If Player 1 has already lost, do not add any more cards and return false
                    {
                        return false;
                    }

                    if (cardCountP1 < 25) // If Player 1 has not reached the maximum card count of 25, add the new card to their hand by updating the corresponding array, combo box items, and card count accordingly
                    {
                        cardsP1[cardCountP1] = newCard;
                        cbP1PlayableCard.Items.Add(newCard);
                        cardCountP1++;
                    }
                    else
                    {
                        loseEvent(gbP1.Text); // If Player 1 has reached the maximum card count of 25, trigger the loseEvent function for Player 1 and return true
                        return true;
                    }
                    continue;
                }

                if (playerName.Contains("Player 2"))
                {
                    if (p2Lost) // If Player 2 has already lost, do not add any more cards and return false
                    {
                        return false;
                    }

                    if (cardCountP2 < 25) // If Player 2 has not reached the maximum card count of 25, add the new card to their hand by updating the corresponding array, combo box items, and card count accordingly
                    {
                        cardsP2[cardCountP2] = newCard;
                        cbP2PlayableCard.Items.Add(newCard);
                        cardCountP2++;
                    }
                    else
                    {
                        loseEvent(gbP2.Text); // If Player 2 has reached the maximum card count of 25, trigger the loseEvent function for Player 2 and return true
                        return true;
                    }
                    continue;
                }

                if (playerName.Contains("Player 3"))
                {
                    if (p3Lost) // If Player 3 has already lost, do not add any more cards and return false
                    {
                        return false;
                    }

                    if (cardCountP3 < 25) // If Player 3 has not reached the maximum card count of 25, add the new card to their hand by updating the corresponding array, combo box items, and card count accordingly
                    {
                        cardsP3[cardCountP3] = newCard;
                        cbP3PlayableCard.Items.Add(newCard);
                        cardCountP3++;
                    }
                    else
                    {
                        loseEvent(gbP3.Text); // If Player 3 has reached the maximum card count of 25, trigger the loseEvent function for Player 3 and return true
                        return true;
                    }
                    continue;
                }

                if (playerName.Contains("Player 4"))
                {
                    if (p4Lost) // If Player 4 has already lost, do not add any more cards and return false
                    {
                        return false;
                    }

                    if (cardCountP4 < 25) // If Player 4 has not reached the maximum card count of 25, add the new card to their hand by updating the corresponding array, combo box items, and card count accordingly
                    {
                        cardsP4[cardCountP4] = newCard;
                        cbP4PlayableCard.Items.Add(newCard);
                        cardCountP4++;
                    }
                    else
                    {
                        loseEvent(gbP4.Text); // If Player 4 has reached the maximum card count of 25, trigger the loseEvent function for Player 4 and return true
                        return true;
                    }
                    continue;
                }
            }

            return false; // return false if no player lost, true if a player lost and the event was triggered
        }

        public void pickupTwoEvent(string currentPlayer, string nextPlayer, string cardId)
        {
            //This function is called when a player plays a Pickup 2 card, it checks which player played the card and updates the game state accordingly by adding two cards to the next player's hand and updating the card count and combo box items accordingly
            if (cardId.Contains("Pickup 2"))
            {
                bool receiverIsLost = addCardToPlayer(getPlayerName(currentPlayer), 2); // Add two cards to the next player's hand by calling the addCardToPlayer function with the current player name and the number of cards to add as parameters)

                if (receiverIsLost) // If the next player has already lost, do not add any more cards and return from the function
                {
                    return;
                }

                // Display a message box to indicate that the next player has to pick up two cards
                MessageBox.Show("2 cards given to the next player!", "Pickup 2", MessageBoxButtons.OK, MessageBoxIcon.Information);

                nextTurnEvent(getPlayerName(currentPlayer)); // Call the nextTurnEvent function with the current player as a parameter to update the game state accordingly
            }
            // Do nothing if the card played is not a Pickup 2 card
            else
            {
                return;
            }
        }

        public void pickupFourEvent(string currentPlayer, string nextPlayer, string cardId)
        {
            //This function is called when a player plays a Pickup Four card, it checks which player played the card and updates the game state accordingly by adding four cards to the next player's hand and updating the card count and combo box items accordingly
            if (cardId.Contains("Pickup 4"))
            {
                bool receiverIsLost = addCardToPlayer(getPlayerName(currentPlayer), 4); // Add four cards to the next player's hand by calling the addCardToPlayer function with the current player name and the number of cards to add as parameters)

                if (receiverIsLost) // If the next player has already lost, do not add any more cards and return from the function
                {
                    return;
                }

                // Display a message box to indicate that the next player has to pick up four cards
                MessageBox.Show("4 cards given to the next player!", "Pickup Four", MessageBoxButtons.OK, MessageBoxIcon.Information);

                nextTurnEvent(getPlayerName(currentPlayer)); // Call the nextTurnEvent function with the current player as a parameter to update the game state accordingly
            }
            // Do nothing if the card played is not a Pickup Four card
            else
            {
                return;
            }
        }



        //FIX ERROR BEHIND THREEPLAYER LOST EVENT NOT WORKING

        public static string wildCardColorChangeMenu()
        {
            // Create a new form that shows the color chose menu
            using (Form colorSelectionForm = new Form())
            {
                // Set the properties of the form such as title, size, and position
                colorSelectionForm.Text = "Select a Color";
                colorSelectionForm.Size = new Size(400, 400);
                colorSelectionForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                colorSelectionForm.StartPosition = FormStartPosition.CenterScreen;
                colorSelectionForm.MaximizeBox = false;
                colorSelectionForm.MinimizeBox = false;
                colorSelectionForm.ControlBox = false;

                // Create buttons for each color option and add click event handlers to set the selected color and close the form when a button is clicked, then add the buttons to the form and show the form as a dialog
                Button redButton = new Button() { Text = "Red", Location = new Point(30, 50), Size = new Size(75, 30) };
                Button greenButton = new Button() { Text = "Green", Location = new Point(110, 50), Size = new Size(75, 30) };
                Button blueButton = new Button() { Text = "Blue", Location = new Point(190, 50), Size = new Size(75, 30) };
                Button yellowButton = new Button() { Text = "Yellow", Location = new Point(270, 50), Size = new Size(75, 30) };
                string selectedColor = null;

                // Add click event handlers to set the selected color and close the form when a button is clicked
                redButton.Click += (s, e) => { selectedColor = "Red"; colorSelectionForm.Close(); };
                greenButton.Click += (s, e) => { selectedColor = "Green"; colorSelectionForm.Close(); };
                blueButton.Click += (s, e) => { selectedColor = "Blue"; colorSelectionForm.Close(); };
                yellowButton.Click += (s, e) => { selectedColor = "Yellow"; colorSelectionForm.Close(); };

                // Add the buttons to the form
                colorSelectionForm.Controls.Add(redButton);
                colorSelectionForm.Controls.Add(greenButton);
                colorSelectionForm.Controls.Add(blueButton);
                colorSelectionForm.Controls.Add(yellowButton);

                // Show the form as a dialog and return the selected color after the form is closed
                colorSelectionForm.ShowDialog();
                return selectedColor;
            }
        }

        string wildCardChosenColor = "";
        public void wildCardColorChangeHandler()
        {
            string selectedColor = wildCardColorChangeMenu();
            wildCardChosenColor = selectedColor; // Set the wildCardChosenColor variable to the selected color for use in the roulette card event handler

            // Update the previousCardPlayed variable based on the selected color to ensure that the next player can only play a card of the same color or a wild card, and display a message box to indicate which color was selected for the wild card
            if (selectedColor != null)
            {
                // Set the previousCardPlayed variable to the corresponding card based on the selected color to ensure that the next player can only play a card of the same color or a wild card, and display a message box to indicate which color was selected for the wild card
                deckSetterAfterWildCard(selectedColor); // Call the deckSetterAfterWildCard function with the selected color as a parameter to update the previousCardPlayed variable and the card deck image accordingly for visual feedback
            }
        }

        public void deckSetterAfterWildCard(string color)
        {
            // This function is called after a wild card is played and a color is selected, it updates the previousCardPlayed variable to a placeholder card of the selected color to ensure that the next player can only play a card of the same color or a wild card, and displays a message box to indicate which color was selected for the wild card
            if (color == "Red")
            {
                previousCardPlayed = "One (Red)";
                pbCardsPlayed.Image = Properties.Resources.red_1; // Update the card deck image to match the selected color for visual feedback
            }
            else if (color == "Green")
            {
                previousCardPlayed = "One (Green)";
                pbCardsPlayed.Image = Properties.Resources.green_1; // Update the card deck image to match the selected color for visual feedback
            }
            else if (color == "Blue")
            {
                previousCardPlayed = "One (Blue)";
                pbCardsPlayed.Image = Properties.Resources.blue_1; // Update the card deck image to match the selected color for visual feedback
            }
            else if (color == "Yellow")
            {
                previousCardPlayed = "One (Yellow)";
                pbCardsPlayed.Image = Properties.Resources.yellow_1; // Update the card deck image to match the selected color for visual feedback
            }
        }

        //FIX ERROR BEHIND THREEPLAYER LOST EVENT NOT WORKING

        public void reversePickupFourEvent(string currentPlayer, string cardId)
        {
            //This function is called when a player plays a Reverse Pickup Four card, it checks which player played the card and updates the game state accordingly by adding four cards to the previous player's hand and updating the card count and combo box items accordingly, and then reversing the direction of play by calling the reverseCardEvent function with the current player name and the card ID as parameters
            if (cardId.Contains("Pickup 4 (Switch)"))
            {
                wildCardColorChangeHandler(); // Call the wildCardColorChangeHandler function to allow the player to select a color for the Reverse Pickup Four card and update the game state accordingly

                // Determine the player who should receive the 4 cards (the "previous" player)
                string recipient = null;
                if (currentPlayer == gbP1.Text)
                {
                    recipient = gbP4.Text; // If Player 1 played the card, the recipient is Player 4 (the previous player in the reversed order of play)
                }
                else if (currentPlayer == gbP2.Text)
                {
                    recipient = gbP1.Text; // If Player 2 played the card, the recipient is Player 1 (the previous player in the reversed order of play)
                }
                else if (currentPlayer == gbP3.Text)
                {
                    recipient = gbP2.Text; // If Player 3 played the card, the recipient is Player 2 (the previous player in the reversed order of play)
                }
                else if (currentPlayer == gbP4.Text)
                {
                    recipient = gbP3.Text; // If Player 4 played the card, the recipient is Player 3 (the previous player in the reversed order of play)
                }
                else
                {
                    // fallback: do nothing if we cannot resolve players
                    return;
                }

                // Display a message box to indicate that the direction of play has been reversed
                MessageBox.Show("The direction of play has been reversed!", "Reverse Direction", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reverseDirection = !reverseDirection; // Reverse the direction of play by toggling the reverseDirection boolean variable
                nextTurnEvent(getPlayerName(currentPlayer)); // Call the nextTurnEvent function with the current player as a parameter to update the game state accordingly based on the new direction of play

                bool receiverIsLost = addCardToPlayer(getPlayerName(currentPlayer), 4); // Add four cards to the previous player's hand by calling the addCardToPlayer function with the current player name and the number of cards to add as parameters)

                if (receiverIsLost) // If the previous player has already lost, do not add any more cards and return from the function
                {
                    return;
                }

                // Display a message box to indicate that the previous player has to pick up four cards
                MessageBox.Show("4 cards given to the previous player!", "Reverse Pickup Four", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            // Do nothing if the card played is not a Reverse Pickup Four card
            else
            {
                return;
            }
        }

        public void pickupSixEvent(string currentPlayer, string nextPlayer, string cardId)
        {
            //This function is called when a player plays a Pickup Six card, it checks which player played the card and updates the game state accordingly by adding six cards to the next player's hand and updating the card count and combo box items accordingly
            if (cardId.Contains("Pickup 6"))
            {
                wildCardColorChangeHandler(); // Call the wildCardColorChangeHandler function to allow the player to select a color for the Pickup Six card and update the game state accordingly

                bool receiverIsLost = addCardToPlayer(getPlayerName(currentPlayer), 6); // Add six cards to the next player's hand by calling the addCardToPlayer function with the current player name and the number of cards to add as parameters)

                if (receiverIsLost) // If the next player has already lost, do not add any more cards and return from the function
                {
                    return;
                }

                // Display a message box to indicate that the next player has to pick up six cards
                MessageBox.Show("6 cards given to the next player!", "Pickup Six", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nextTurnEvent(getPlayerName(currentPlayer)); // Call the nextTurnEvent function with the current player as a parameter to update the game state accordingly
            }
            // Do nothing if the card played is not a Pickup Six card
            else
            {
                return;
            }
        }

        public void pickupTenEvent(string currentPlayer, string nextPlayer, string cardId)
        {
            //This function is called when a player plays a Pickup Ten card, it checks which player played the card and updates the game state accordingly by adding ten cards to the next player's hand and updating the card count and combo box items accordingly
            if (cardId.Contains("Pickup 10"))
            {
                wildCardColorChangeHandler(); // Call the wildCardColorChangeHandler function to allow the player to select a color for the Pickup Ten card and update the game state accordingly

                bool receiverIsLost = addCardToPlayer(getPlayerName(currentPlayer), 10); // Add ten cards to the next player's hand by calling the addCardToPlayer function with the current player name and the number of cards to add as parameters)

                if (receiverIsLost) // If the next player has already lost, do not add any more cards and return from the function
                {
                    return;
                }

                // Display a message box to indicate that the next player has to pick up ten cards
                MessageBox.Show("10 cards given to the next player!", "Pickup Ten", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nextTurnEvent(getPlayerName(currentPlayer)); // Call the nextTurnEvent function with the current player as a parameter to update the game state accordingly
            }
            // Do nothing if the card played is not a Pickup Ten card
            else
            {
                return;
            }
        }
        public void rouletteCardEvent(string currentPlayer, string nextPlayer, string cardId)
        {
            //This function is called when a player plays a Roulette card, it checks which player played the card and updates the game state accordingly by randomly selecting a number between 1 and 10 and adding that many cards to the next player's hand, and updating the card count and combo box items accordingly
            if (cardId.Contains("Roulette"))
            {
                int cardsGiven = 1; // Initialize a variable to keep track of how many cards have been given to the next player

                wildCardColorChangeHandler(); // Call the wildCardColorChangeHandler function to allow the player to select a color for the Roulette card and update the game state accordingly

                // Loop through the next player's hand and check if the picked card matches the color chosen by the player, if it does not match, add one card to the next player's hand and increment the cardsGiven variable, if it does match, break out of the loop and stop giving cards to the next player
                for (int i = 0; i < 25; i++)
                {
                    bool receiverIsLost = addCardToPlayer(getPlayerName(currentPlayer), 1); // Add one card to the next player's hand by calling the addCardToPlayer function with the current player name and the number of cards to add as parameters

                    if (receiverIsLost) // If the next player has already lost, do not add any more cards and return from the function
                    {
                        return;
                    }

                    // Check if the picked card matches the color chosen by the player, if it does not match, increment the cardsGiven variable, if it does match, break out of the loop and stop giving cards to the next player
                    if (!(rouletteNewCardID.Contains(wildCardChosenColor))) // Check if the picked card matches the color chosen by the player
                    {
                        cardsGiven++; // Increment the cardsGiven variable to keep track of how many cards have been given to the next player
                    }
                    else
                    {
                        deckSetterAfterWildCard(wildCardChosenColor); // Call the deckSetterAfterWildCard function with the color chosen by the player as a parameter to update the previousCardPlayed variable and the card deck image accordingly for visual feedback
                        break; // If the picked card matches the color chosen by the player, break out of the loop and stop giving cards to the next player
                    }
                }
                // Display a message box to indicate how many cards were given to the next player based on the number of cards that were added to their hand
                MessageBox.Show(cardsGiven + " cards given to the next player based on chosing the color " + wildCardChosenColor, "Roulette", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nextTurnEvent(getPlayerName(currentPlayer)); // Call the nextTurnEvent function with the current player as a parameter to update the game state accordingly
            }
            // Do nothing if the card played is not a Roulette card
            else
            {
                return;
            }
        }

        bool sevenCardHandlerInvalidCase; // Designed to catch the invalid cases and prevent the turn from ending if so (False for invalid, True for valid)

        public string sevenSwapCardsHandler(string playerPlayed, string chosenPlayer, string cardId)
        {
            string[] currentPlayerHand = new string[25]; // Initialize an array to store the current player's hand for use in swapping hands with the chosen player based on the player's selection
            string[] chosenPlayerHand = new string[25]; // Initialize an array to store the chosen player's hand for use in swapping hands with the current player based on the player's selection

            int currentPlayerCardCount = 0; // Initialize a variable to keep track of the current player's card count for use in swapping hands with the chosen player based on the player's selection
            int chosenPlayerCardCount = 0; // Initialize a variable to keep track of the chosen player's card count for use in swapping hands with the current player based on the player's selection

            // check if player is choosing themselve to swap hands with, if they are, display a message box to indicate that they cannot swap hands with themselves and prompt them to choose a different player, and return the finalPlayer variable which will be used in the sevenCardChosenSwapEvent function to determine which player's hand to swap with based on the player's selection
            if (playerPlayed == gbP1.Text && chosenPlayer == gbP1.Text)
            {
                MessageBox.Show("You cannot swap hands with yourself! Please choose a different player.", "Invalid Player Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sevenCardHandlerInvalidCase = false;
            }
            else if (playerPlayed == gbP2.Text && chosenPlayer == gbP2.Text)
            {
                MessageBox.Show("You cannot swap hands with yourself! Please choose a different player.", "Invalid Player Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sevenCardHandlerInvalidCase = false;
            }
            else if (playerPlayed == gbP3.Text && chosenPlayer == gbP3.Text)
            {
                MessageBox.Show("You cannot swap hands with yourself! Please choose a different player.", "Invalid Player Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sevenCardHandlerInvalidCase = false;
                return "Invalid";
            }
            else if (playerPlayed == gbP4.Text && chosenPlayer == gbP4.Text)
            {
                MessageBox.Show("You cannot swap hands with yourself! Please choose a different player.", "Invalid Player Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sevenCardHandlerInvalidCase = false;
            }
            // if not, swap hands with the chosen player and return the finalPlayer variable which will be used in the sevenCardChosenSwapEvent function to determine which player's hand to swap with based on the player's selection
            else if (playerPlayed == gbP1.Text && chosenPlayer == gbP2.Text)
            {

                currentPlayerCardCount = cardCountP1; // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                chosenPlayerCardCount = cardCountP2;// Set the chosen player's card count to the corresponding variable based on the chosenPlayer parameter for use in swapping hands with the current player based on the player's selection

                for (int i = 0; i < cardCountP1; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the chosen player based on the player's selection
                {
                    currentPlayerHand[i] = cardsP1[i];
                    cardsP1[i] = "";
                    cbP1PlayableCard.Items.Clear();
                }

                for (int i = 0; i < cardCountP2; i++) // Loop through the chosen player's hand and store the cards in the chosenPlayerHand array, clear the chosen player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                {
                    chosenPlayerHand[i] = cardsP2[i];
                    cardsP2[i] = "";
                    cbP2PlayableCard.Items.Clear();
                }

                for (int i = 0; i < currentPlayerCardCount; i++)
                {
                    if (currentPlayerHand[i].Contains(cardId) && !string.IsNullOrEmpty(currentPlayerHand[i]))
                    {
                        currentPlayerHand[i] = currentPlayerHand[i + 1]; // Remove the card from the current player's hand if it matches the cardId parameter and is not null or empty
                    }
                }
                currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to null to remove it from the hand after swapping hands with the chosen player based on the player's selection
                currentPlayerCardCount--; // Decrement the current player's card count to reflect the removal of the card from their hand after swapping hands with the chosen player based on the player's selection

                for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the chosen player by assigning the values from the currentPlayerHand array to the chosen player's card array and the values from the chosenPlayerHand array to the current player's card array based on the player's selection
                {
                    cardsP2[i] = currentPlayerHand[i];
                    cardsP1[i] = chosenPlayerHand[i];
                }

                cardCountP1 = chosenPlayerCardCount; // Set the current player's card count to the chosen player's card count and the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                cardCountP2 = currentPlayerCardCount; // Set the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection

                for (int i = 0; i < cardCountP1; i++) // Loop through the current player's card array and add the cards to the current player's combo box items to update the game state visually after swapping hands with the chosen player based on the player's selection
                {
                    if (cardsP1[i] != null)
                    {
                        cbP1PlayableCard.Items.Add(cardsP1[i]);
                    }
                }
                for (int i = 0; i < cardCountP2; i++) // Loop through the chosen player's card array and add the cards to the chosen player's combo box items to update the game state visually after swapping hands with the current player based on the player's selection
                {
                    if (cardsP2[i] != null)
                    {
                        cbP2PlayableCard.Items.Add(cardsP2[i]);
                    }
                }
                sevenCardHandlerInvalidCase = true;
            }
            else if (playerPlayed == gbP1.Text && chosenPlayer == gbP3.Text)
            {
                currentPlayerCardCount = cardCountP1; // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                chosenPlayerCardCount = cardCountP3;// Set the chosen player's card count to the corresponding variable based on the chosenPlayer parameter for use in swapping hands with the current player based on the player's selection

                for (int i = 0; i < cardCountP1; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the chosen player based on the player's selection
                {
                    currentPlayerHand[i] = cardsP1[i];
                    cardsP1[i] = "";
                    cbP1PlayableCard.Items.Clear();
                }
                for (int i = 0; i < cardCountP3; i++) // Loop through the chosen player's hand and store the cards in the chosenPlayerHand array, clear the chosen player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                {
                    chosenPlayerHand[i] = cardsP3[i];
                    cardsP3[i] = "";
                    cbP3PlayableCard.Items.Clear();
                }

                for (int i = 0; i < currentPlayerCardCount; i++)
                {
                    if (currentPlayerHand[i].Contains(cardId) && !string.IsNullOrEmpty(currentPlayerHand[i]))
                    {
                        currentPlayerHand[i] = currentPlayerHand[i + 1]; // Remove the card from the current player's hand if it matches the cardId parameter and is not null or empty
                    }
                }
                currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to null to remove it from the hand after swapping hands with the chosen player based on the player's selection
                currentPlayerCardCount--; // Decrement the current player's card count to reflect the removal of the card from their hand after swapping hands with the chosen player based on the player's selection

                for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the chosen player by assigning the values from the currentPlayerHand array to the chosen player's card array and the values from the chosenPlayerHand array to the current player's card array based on the player's selection
                {
                    cardsP3[i] = currentPlayerHand[i];
                    cardsP1[i] = chosenPlayerHand[i];
                }
                cardCountP1 = chosenPlayerCardCount; // Set the current player's card count to the chosen player's card count and the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                cardCountP3 = currentPlayerCardCount; // Set the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                for (int i = 0; i < cardCountP1; i++) // Loop through the current player's card array and add the cards to the chosen player's combo box
                {
                    if (cardsP1[i] != null)
                    {
                        cbP1PlayableCard.Items.Add(cardsP1[i]);
                    }
                }
                for (int i = 0; i < cardCountP3; i++) // Loop through the chosen player's card array and add the cards to the current player's combo box
                {
                    if (cardsP3[i] != null)
                    {
                        cbP3PlayableCard.Items.Add(cardsP3[i]);
                    }
                }
                sevenCardHandlerInvalidCase = true;
            }
            else if (playerPlayed == gbP1.Text && chosenPlayer == gbP4.Text)
            {
                currentPlayerCardCount = cardCountP1; // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                chosenPlayerCardCount = cardCountP4;// Set the chosen player's card count to the corresponding variable based on the chosenPlayer parameter for use in swapping hands with the current player based on the player's selection
                for (int i = 0; i < cardCountP1; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the chosen player based on the player's selection
                {
                    currentPlayerHand[i] = cardsP1[i];
                    cardsP1[i] = "";
                    cbP1PlayableCard.Items.Clear();
                }
                for (int i = 0; i < cardCountP4; i++) // Loop through the chosen player's hand and store the cards in the chosenPlayerHand array, clear the chosen player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                {
                    chosenPlayerHand[i] = cardsP4[i];
                    cardsP4[i] = "";
                    cbP4PlayableCard.Items.Clear();
                }

                for (int i = 0; i < currentPlayerCardCount; i++)
                {
                    if (currentPlayerHand[i].Contains(cardId) && !string.IsNullOrEmpty(currentPlayerHand[i]))
                    {
                        currentPlayerHand[i] = currentPlayerHand[i + 1]; // Remove the card from the current player's hand if it matches the cardId parameter and is not null or empty
                    }
                }
                currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to null to remove it from the hand after swapping hands with the chosen player based on the player's selection
                currentPlayerCardCount--; // Decrement the current player's card count to reflect the removal of the card from their hand after swapping hands with the chosen player based on the player's selection

                for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the chosen player by assigning the values from the currentPlayerHand array to the chosen player's card array and the values from the chosenPlayerHand array to the current player's card array based on the player's selection
                {
                    cardsP4[i] = currentPlayerHand[i];
                    cardsP1[i] = chosenPlayerHand[i];
                }
                cardCountP1 = chosenPlayerCardCount; // Set the current player's card count to the chosen player's card count and the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                cardCountP4 = currentPlayerCardCount; // Set the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection

                for (int i = 0; i < cardCountP1; i++) // Loop through the current player's card array and add the cards to the chosen player's combo box
                {
                    if (cardsP1[i] != null)
                    {
                        cbP1PlayableCard.Items.Add(cardsP1[i]);
                    }
                }
                for (int i = 0; i < cardCountP4; i++)
                {
                    if (cardsP4[i] != null)
                    {
                        cbP4PlayableCard.Items.Add(cardsP4[i]);
                    }
                }
                sevenCardHandlerInvalidCase = true;
            }
            else if (playerPlayed == gbP2.Text && chosenPlayer == gbP1.Text)
            {
                currentPlayerCardCount = cardCountP2; // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                chosenPlayerCardCount = cardCountP1;// Set the chosen player's card count to the corresponding variable based on the chosenPlayer parameter for use in swapping hands with the current player based on the player's selection
                for (int i = 0; i < cardCountP2; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the chosen player based on the player's selection
                {
                    currentPlayerHand[i] = cardsP2[i];
                    cardsP2[i] = "";
                    cbP2PlayableCard.Items.Clear();
                }
                for (int i = 0; i < cardCountP1; i++) // Loop through the chosen player's hand and store the cards in the chosenPlayerHand array, clear the chosen player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                {
                    chosenPlayerHand[i] = cardsP1[i];
                    cardsP1[i] = "";
                    cbP1PlayableCard.Items.Clear();
                }

                for (int i = 0; i < currentPlayerCardCount; i++)
                {
                    if (currentPlayerHand[i].Contains(cardId) && !string.IsNullOrEmpty(currentPlayerHand[i]))
                    {
                        currentPlayerHand[i] = currentPlayerHand[i + 1]; // Remove the card from the current player's hand if it matches the cardId parameter and is not null or empty
                    }
                }
                currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to null to remove it from the hand after swapping hands with the chosen player based on the player's selection
                currentPlayerCardCount--; // Decrement the current player's card count to reflect the removal of the card from their hand after swapping hands with the chosen player based on the player's selection

                for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the chosen player by assigning the values from the currentPlayerHand array to the chosen player's card array and the values from the chosenPlayerHand array to the current player's card array based on the player's selection
                {
                    cardsP1[i] = currentPlayerHand[i];
                    cardsP2[i] = chosenPlayerHand[i];
                }
                cardCountP2 = chosenPlayerCardCount; // Set the current player's card count to the chosen player's card count and the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                cardCountP1 = currentPlayerCardCount; // Set the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection

                for (int i = 0; i < cardCountP2; i++) // Loop through the current player's card array and add
                {
                    if (cardsP2[i] != null)
                    {
                        cbP2PlayableCard.Items.Add(cardsP2[i]);
                    }
                }
                for (int i = 0; i < cardCountP1; i++) // Loop through the chosen player's card array and add the cards to the current player's combo box
                {
                    if (cardsP1[i] != null)
                    {
                        cbP1PlayableCard.Items.Add(cardsP1[i]);
                    }
                }
                sevenCardHandlerInvalidCase = true;
            }
            else if (playerPlayed == gbP2.Text && chosenPlayer == gbP3.Text)
            {
                currentPlayerCardCount = cardCountP2; // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                chosenPlayerCardCount = cardCountP3;// Set the chosen player's card count to the corresponding variable based on the chosenPlayer parameter for use in swapping hands with the current player based on the player's selection
                for (int i = 0; i < cardCountP2; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the chosen player based on the player's selection
                {
                    currentPlayerHand[i] = cardsP2[i];
                    cardsP2[i] = "";
                    cbP2PlayableCard.Items.Clear();
                }
                for (int i = 0; i < cardCountP3; i++) // Loop through the chosen player's hand and store the cards in the chosenPlayerHand array, clear the chosen player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                {
                    chosenPlayerHand[i] = cardsP3[i];
                    cardsP3[i] = "";
                    cbP3PlayableCard.Items.Clear();
                }

                for (int i = 0; i < currentPlayerCardCount; i++)
                {
                    if (currentPlayerHand[i].Contains(cardId) && !string.IsNullOrEmpty(currentPlayerHand[i]))
                    {
                        currentPlayerHand[i] = currentPlayerHand[i + 1]; // Remove the card from the current player's hand if it matches the cardId parameter and is not null or empty
                    }
                }
                currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to null to remove it from the hand after swapping hands with the chosen player based on the player's selection
                currentPlayerCardCount--; // Decrement the current player's card count to reflect the removal of the card from their hand after swapping hands with the chosen player based on the player's selection

                for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the chosen player by assigning the values from the currentPlayerHand array to the chosen player's card array and the values from the chosenPlayerHand array to the current player's card array based on the player's selection
                {
                    cardsP3[i] = currentPlayerHand[i];
                    cardsP2[i] = chosenPlayerHand[i];
                }
                cardCountP2 = chosenPlayerCardCount; // Set the current player's card count to the chosen player's card count and the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                cardCountP3 = currentPlayerCardCount; // Set the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                for (int i = 0; i < cardCountP2; i++) // Loop through the current player's card array and add the cards to the combo box
                {
                    if (cardsP2[i] != null)
                    {
                        cbP2PlayableCard.Items.Add(cardsP2[i]);
                    }
                }
                for (int i = 0; i < cardCountP3; i++) // Loop through the chosen player's card array and add the cards to the combo box
                {
                    if (cardsP3[i] != null)
                    {
                        cbP3PlayableCard.Items.Add(cardsP3[i]);
                    }
                }
                sevenCardHandlerInvalidCase = true;
            }
            else if (playerPlayed == gbP2.Text && chosenPlayer == gbP4.Text)
            {
                currentPlayerCardCount = cardCountP2; // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                chosenPlayerCardCount = cardCountP4;// Set the chosen player's card count to the corresponding variable based on the chosenPlayer parameter for use in swapping hands with the current player based on the player's selection
                for (int i = 0; i < cardCountP2; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the chosen player based on the player's selection
                {
                    currentPlayerHand[i] = cardsP2[i];
                    cardsP2[i] = "";
                    cbP2PlayableCard.Items.Clear();
                }
                for (int i = 0; i < cardCountP4; i++) // Loop through the chosen player's hand and store the cards in the chosenPlayerHand array, clear the chosen player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                {
                    chosenPlayerHand[i] = cardsP4[i];
                    cardsP4[i] = "";
                    cbP4PlayableCard.Items.Clear();
                }

                for (int i = 0; i < currentPlayerCardCount; i++)
                {
                    if (currentPlayerHand[i].Contains(cardId) && !string.IsNullOrEmpty(currentPlayerHand[i]))
                    {
                        currentPlayerHand[i] = currentPlayerHand[i + 1]; // Remove the card from the current player's hand if it matches the cardId parameter and is not null or empty
                    }
                }
                currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to null to remove it from the hand after swapping hands with the chosen player based on the player's selection
                currentPlayerCardCount--; // Decrement the current player's card count to reflect the removal of the card from their hand after swapping hands with the chosen player based on the player's selection

                for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the chosen player by assigning the values from the currentPlayerHand array to the chosen player's card array and the values from the chosenPlayerHand array to the current player's card array based on the player's selection
                {
                    cardsP4[i] = currentPlayerHand[i];
                    cardsP2[i] = chosenPlayerHand[i];
                }
                cardCountP2 = chosenPlayerCardCount; // Set the current player's card count to the chosen player's card count and the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                cardCountP4 = currentPlayerCardCount; // Set the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                for (int i = 0; i < cardCountP2; i++) // Loop through the current player's card array and add the cards to the combo box
                {
                    if (cardsP2[i] != null)
                    {
                        cbP2PlayableCard.Items.Add(cardsP2[i]);
                    }
                }
                for (int i = 0; i < cardCountP4; i++) // Loop through the chosen player's card array and add the cards to the combo box
                {
                    if (cardsP4[i] != null)
                    {
                        cbP4PlayableCard.Items.Add(cardsP4[i]);
                    }
                }
                sevenCardHandlerInvalidCase = true;
            }
            else if (playerPlayed == gbP3.Text && chosenPlayer == gbP1.Text)
            {
                currentPlayerCardCount = cardCountP3; // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                chosenPlayerCardCount = cardCountP1;// Set the chosen player's card count to the corresponding variable based on the chosenPlayer parameter for use in swapping hands with the current player based on the player's selection
                for (int i = 0; i < cardCountP3; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the chosen player based on the player's selection
                {
                    currentPlayerHand[i] = cardsP3[i];
                    cardsP3[i] = "";
                    cbP3PlayableCard.Items.Clear();
                }
                for (int i = 0; i < cardCountP1; i++) // Loop through the chosen player's hand and store the cards in the chosenPlayerHand array, clear the chosen player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                {
                    chosenPlayerHand[i] = cardsP1[i];
                    cardsP1[i] = "";
                    cbP1PlayableCard.Items.Clear();
                }

                for (int i = 0; i < currentPlayerCardCount; i++)
                {
                    if (currentPlayerHand[i].Contains(cardId) && !string.IsNullOrEmpty(currentPlayerHand[i]))
                    {
                        currentPlayerHand[i] = currentPlayerHand[i + 1]; // Remove the card from the current player's hand if it matches the cardId parameter and is not null or empty
                    }
                }
                currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to null to remove it from the hand after swapping hands with the chosen player based on the player's selection
                currentPlayerCardCount--; // Decrement the current player's card count to reflect the removal of the card from their hand after swapping hands with the chosen player based on the player's selection

                for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the chosen player by assigning the values from the currentPlayerHand array to the chosen player's card array and the values from the chosenPlayerHand array to the current player's card array based on the player's selection
                {
                    cardsP1[i] = currentPlayerHand[i];
                    cardsP3[i] = chosenPlayerHand[i];
                }
                cardCountP3 = chosenPlayerCardCount; // Set the current player's card count to the chosen player's card count and the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                cardCountP1 = currentPlayerCardCount; // Set the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                for (int i = 0; i < cardCountP3; i++) // Loop through the current player's card array and add the cards to the combo box
                {
                    if (cardsP3[i] != null)
                    {
                        cbP3PlayableCard.Items.Add(cardsP3[i]);
                    }
                }
                for (int i = 0; i < cardCountP1; i++) // Loop through the chosen player's card array and add the cards to the combo box
                {
                    if (cardsP1[i] != null)
                    {
                        cbP1PlayableCard.Items.Add(cardsP1[i]);
                    }
                }
                sevenCardHandlerInvalidCase = true;
            }
            else if (playerPlayed == gbP3.Text && chosenPlayer == gbP2.Text)
            {
                currentPlayerCardCount = cardCountP3; // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                chosenPlayerCardCount = cardCountP2;// Set the chosen player's card count to the corresponding variable based on the chosenPlayer parameter for use in swapping hands with the current player based on the player's selection
                for (int i = 0; i < cardCountP3; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the chosen player based on the player's selection
                {
                    currentPlayerHand[i] = cardsP3[i];
                    cardsP3[i] = "";
                    cbP3PlayableCard.Items.Clear();
                }
                for (int i = 0; i < cardCountP2; i++) // Loop through the chosen player's hand and store the cards in the chosenPlayerHand array, clear the chosen player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                {
                    chosenPlayerHand[i] = cardsP2[i];
                    cardsP2[i] = "";
                    cbP2PlayableCard.Items.Clear();
                }

                for (int i = 0; i < currentPlayerCardCount; i++)
                {
                    if (currentPlayerHand[i].Contains(cardId) && !string.IsNullOrEmpty(currentPlayerHand[i]))
                    {
                        currentPlayerHand[i] = currentPlayerHand[i + 1]; // Remove the card from the current player's hand if it matches the cardId parameter and is not null or empty
                    }
                }
                currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to null to remove it from the hand after swapping hands with the chosen player based on the player's selection
                currentPlayerCardCount--; // Decrement the current player's card count to reflect the removal of the card from their hand after swapping hands with the chosen player based on the player's selection

                for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the chosen player by assigning the values from the currentPlayerHand array to the chosen player's card array and the values from the chosenPlayerHand array to the current player's card array based on the player's selection
                {
                    cardsP2[i] = currentPlayerHand[i];
                    cardsP3[i] = chosenPlayerHand[i];
                }
                cardCountP3 = chosenPlayerCardCount; // Set the current player's card count to the chosen player's card count and the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                cardCountP2 = currentPlayerCardCount; // Set the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                for (int i = 0; i < cardCountP3; i++) // Loop through the current player's card array and add the cards to the combo box
                {
                    if (cardsP3[i] != null)
                    {
                        cbP3PlayableCard.Items.Add(cardsP3[i]);
                    }
                }
                for (int i = 0; i < cardCountP2; i++) // Loop through the chosen player's card array and add the cards to the combo box
                {
                    if (cardsP2[i] != null)
                    {
                        cbP2PlayableCard.Items.Add(cardsP2[i]);
                    }
                }
                sevenCardHandlerInvalidCase = true;
            }
            else if (playerPlayed == gbP3.Text && chosenPlayer == gbP4.Text)
            {
                currentPlayerCardCount = cardCountP3; // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                chosenPlayerCardCount = cardCountP4;// Set the chosen player's card count to the corresponding variable based on the chosenPlayer parameter for use in swapping hands with the current player based on the player's selection
                for (int i = 0; i < cardCountP3; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the chosen player based on the player's selection
                {
                    currentPlayerHand[i] = cardsP3[i];
                    cardsP3[i] = "";
                    cbP3PlayableCard.Items.Clear();
                }
                for (int i = 0; i < cardCountP4; i++) // Loop through the chosen player's hand and store the cards in the chosenPlayerHand array, clear the chosen player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                {
                    chosenPlayerHand[i] = cardsP4[i];
                    cardsP4[i] = "";
                    cbP4PlayableCard.Items.Clear();
                }

                for (int i = 0; i < currentPlayerCardCount; i++)
                {
                    if (currentPlayerHand[i].Contains(cardId) && !string.IsNullOrEmpty(currentPlayerHand[i]))
                    {
                        currentPlayerHand[i] = currentPlayerHand[i + 1]; // Remove the card from the current player's hand if it matches the cardId parameter and is not null or empty
                    }
                }
                currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to null to remove it from the hand after swapping hands with the chosen player based on the player's selection
                currentPlayerCardCount--; // Decrement the current player's card count to reflect the removal of the card from their hand after swapping hands with the chosen player based on the player's selection

                for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the chosen player by assigning the values from the currentPlayerHand array to the chosen player's card array and the values from the chosenPlayerHand array to the current player's card array based on the player's selection
                {
                    cardsP4[i] = currentPlayerHand[i];
                    cardsP3[i] = chosenPlayerHand[i];
                }
                cardCountP3 = chosenPlayerCardCount; // Set the current player's card count to the chosen player's card count and the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                cardCountP4 = currentPlayerCardCount; // Set the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                for (int i = 0; i < cardCountP3; i++) // Loop through the current player's card array and add the cards to the combo box
                {
                    if (cardsP3[i] != null)
                    {
                        cbP3PlayableCard.Items.Add(cardsP3[i]);
                    }
                }
                for (int i = 0; i < cardCountP4; i++) // Loop through the chosen player's card array and add the cards to the combo box
                {
                    if (cardsP4[i] != null)
                    {
                        cbP4PlayableCard.Items.Add(cardsP4[i]);
                    }
                }
                sevenCardHandlerInvalidCase = true;
            }
            else if (playerPlayed == gbP4.Text && chosenPlayer == gbP1.Text)
            {
                currentPlayerCardCount = cardCountP4; // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                chosenPlayerCardCount = cardCountP1;// Set the chosen player's card count to the corresponding variable based on the chosenPlayer parameter for use in swapping hands with the current player based on the player's selection
                for (int i = 0; i < cardCountP4; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the chosen player based on the player's selection
                {
                    currentPlayerHand[i] = cardsP4[i];
                    cardsP4[i] = "";
                    cbP4PlayableCard.Items.Clear();
                }
                for (int i = 0; i < cardCountP1; i++) // Loop through the chosen player's hand and store the cards in the chosenPlayerHand array, clear the chosen player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                {
                    chosenPlayerHand[i] = cardsP1[i];
                    cardsP1[i] = "";
                    cbP1PlayableCard.Items.Clear();
                }

                for (int i = 0; i < currentPlayerCardCount; i++)
                {
                    if (currentPlayerHand[i].Contains(cardId) && !string.IsNullOrEmpty(currentPlayerHand[i]))
                    {
                        currentPlayerHand[i] = currentPlayerHand[i + 1]; // Remove the card from the current player's hand if it matches the cardId parameter and is not null or empty
                    }
                }
                currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to null to remove it from the hand after swapping hands with the chosen player based on the player's selection
                currentPlayerCardCount--; // Decrement the current player's card count to reflect the removal of the card from their hand after swapping hands with the chosen player based on the player's selection

                for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the chosen player by assigning the values from the currentPlayerHand array to the chosen player's card array and the values from the chosenPlayerHand array to the current player's card array based on the player's selection
                {
                    cardsP1[i] = currentPlayerHand[i];
                    cardsP4[i] = chosenPlayerHand[i];
                }
                cardCountP4 = chosenPlayerCardCount; // Set the current player's card count to the chosen player's card count and the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                cardCountP1 = currentPlayerCardCount; // Set the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                for (int i = 0; i < cardCountP4; i++) // Loop through the current player's card array and add the cards to the combo box
                {
                    if (cardsP4[i] != null)
                    {
                        cbP4PlayableCard.Items.Add(cardsP4[i]);
                    }
                }
                for (int i = 0; i < cardCountP1; i++) // Loop through the chosen player's card array and add the cards to the combo box
                {
                    if (cardsP1[i] != null)
                    {
                        cbP1PlayableCard.Items.Add(cardsP1[i]);
                    }
                }
                sevenCardHandlerInvalidCase = true;
            }
            else if (playerPlayed == gbP4.Text && chosenPlayer == gbP2.Text)
            {
                currentPlayerCardCount = cardCountP4; // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                chosenPlayerCardCount = cardCountP2;// Set the chosen player's card count to the corresponding variable based on the chosenPlayer parameter for use in swapping hands with the current player based on the player's selection
                for (int i = 0; i < cardCountP4; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the chosen player based on the player's selection
                {
                    currentPlayerHand[i] = cardsP4[i];
                    cardsP4[i] = "";
                    cbP4PlayableCard.Items.Clear();
                }
                for (int i = 0; i < cardCountP2; i++) // Loop through the chosen player's hand and store the cards in the chosenPlayerHand array, clear the chosen player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                {
                    chosenPlayerHand[i] = cardsP2[i];
                    cardsP2[i] = "";
                    cbP2PlayableCard.Items.Clear();
                }

                for (int i = 0; i < currentPlayerCardCount; i++)
                {
                    if (currentPlayerHand[i].Contains(cardId) && !string.IsNullOrEmpty(currentPlayerHand[i]))
                    {
                        currentPlayerHand[i] = currentPlayerHand[i + 1]; // Remove the card from the current player's hand if it matches the cardId parameter and is not null or empty
                    }
                }
                currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to null to remove it from the hand after swapping hands with the chosen player based on the player's selection
                currentPlayerCardCount--; // Decrement the current player's card count to reflect the removal of the card from their hand after swapping hands with the chosen player based on the player's selection

                for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the chosen player by assigning the values from the currentPlayerHand array to the chosen player's card array and the values from the chosenPlayerHand array to the current player's card array based on the player's selection
                {
                    cardsP2[i] = currentPlayerHand[i];
                    cardsP4[i] = chosenPlayerHand[i];
                }
                cardCountP4 = chosenPlayerCardCount; // Set the current player's card count to the chosen player's card count and the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                cardCountP2 = currentPlayerCardCount; // Set the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                for (int i = 0; i < cardCountP4; i++) // Loop through the current player's card array and add the cards to the combo box
                {
                    if (cardsP4[i] != null)
                    {
                        cbP4PlayableCard.Items.Add(cardsP4[i]);
                    }
                }
                for (int i = 0; i < cardCountP2; i++) // Loop through the chosen player's card array and add the cards to the combo box
                {
                    if (cardsP2[i] != null)
                    {
                        cbP2PlayableCard.Items.Add(cardsP2[i]);
                    }
                }
                sevenCardHandlerInvalidCase = true;
            }
            else if (playerPlayed == gbP4.Text && chosenPlayer == gbP3.Text)
            {
                currentPlayerCardCount = cardCountP4; // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                chosenPlayerCardCount = cardCountP3;// Set the chosen player's card count to the corresponding variable based on the chosenPlayer parameter for use in swapping hands with the current player based on the player's selection
                for (int i = 0; i < cardCountP4; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the chosen player based on the player's selection
                {
                    currentPlayerHand[i] = cardsP4[i];
                    cardsP4[i] = "";
                    cbP4PlayableCard.Items.Clear();
                }
                for (int i = 0; i < cardCountP3; i++) // Loop through the chosen player's hand and store the cards in the chosenPlayerHand array, clear the chosen player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                {
                    chosenPlayerHand[i] = cardsP3[i];
                    cardsP3[i] = "";
                    cbP3PlayableCard.Items.Clear();
                }

                for (int i = 0; i < currentPlayerCardCount; i++)
                {
                    if (currentPlayerHand[i].Contains(cardId) && !string.IsNullOrEmpty(currentPlayerHand[i]))
                    {
                        currentPlayerHand[i] = currentPlayerHand[i + 1]; // Remove the card from the current player's hand if it matches the cardId parameter and is not null or empty
                    }
                }
                currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to null to remove it from the hand after swapping hands with the chosen player based on the player's selection
                currentPlayerCardCount--; // Decrement the current player's card count to reflect the removal of the card from their hand after swapping hands with the chosen player based on the player's selection

                for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the chosen player by assigning the values from the currentPlayerHand array to the chosen player's card array and the values from the chosenPlayerHand array to the current player's card array based on the player's selection
                {
                    cardsP3[i] = currentPlayerHand[i];
                    cardsP4[i] = chosenPlayerHand[i];
                }
                cardCountP4 = chosenPlayerCardCount; // Set the current player's card count to the chosen player's card count and the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                cardCountP3 = currentPlayerCardCount; // Set the chosen player's card count to the current player's card count to complete the swap of hands between the current player and the chosen player based on the player's selection
                for (int i = 0; i < cardCountP4; i++) // Loop through the current player's card array and add the cards to the combo box
                {
                    if (cardsP4[i] != null)
                    {
                        cbP4PlayableCard.Items.Add(cardsP4[i]);
                    }
                }
                for (int i = 0; i < cardCountP3; i++) // Loop through the chosen player's card array and add the cards to the combo box
                {
                    if (cardsP3[i] != null)
                    {
                        cbP3PlayableCard.Items.Add(cardsP3[i]);
                    }
                }
                sevenCardHandlerInvalidCase = true;
            }
            if (sevenCardHandlerInvalidCase == true)
            {
                MessageBox.Show("You have swapped hands with " + chosenPlayer + "!", "Seven Card Swap", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return chosenPlayer; // Return the chosen player name for use in the sevenCardChosenSwapEvent function to determine which player's hand to swap with based on the player's selection
        }

        public void sevenCardChosenSwapEvent(string currentPlayer, string nextPlayer, string cardId)
        {
            //This function is called when a player plays a Seven card, it checks which player played the card and updates the game state accordingly by allowing the player to choose another player to swap hands with, and then swapping the card arrays, combo box items, and card counts of the current player and the chosen player to update the game state accordingly
            if (cardId.Contains("Seven"))
            {
                Form playerSwapForm = new Form(); // Create the form to show the menu
                // Create a new form that shows the player swap menu
                using (playerSwapForm)
                {
                    // Set the properties of the form such as title, size, and position
                    playerSwapForm.Text = "Select a Player to Swap With";
                    playerSwapForm.Size = new Size(400, 400);
                    playerSwapForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    playerSwapForm.StartPosition = FormStartPosition.CenterScreen;
                    playerSwapForm.MaximizeBox = false;
                    playerSwapForm.MinimizeBox = false;
                    playerSwapForm.ControlBox = false;
                    // Create buttons for each player option and add click event handlers to set the selected player and close the form when a button is clicked, then add the buttons to the form and show the form as a dialog
                    Button p1Button = new Button() { Text = gbP1.Text, Location = new Point(30, 50), Size = new Size(75, 30) };
                    Button p2Button = new Button() { Text = gbP2.Text, Location = new Point(110, 50), Size = new Size(75, 30) };
                    Button p3Button = new Button() { Text = gbP3.Text, Location = new Point(190, 50), Size = new Size(75, 30) };
                    Button p4Button = new Button() { Text = gbP4.Text, Location = new Point(270, 50), Size = new Size(75, 30) };
                    string selectedPlayer = null;
                    // Add click event handlers to set the selected player and close the form when a button is clicked
                    p1Button.Click += (s, e) => { selectedPlayer = sevenSwapCardsHandler(currentPlayer, p1Button.Text, cardId); if (sevenCardHandlerInvalidCase == true) playerSwapForm.Close(); };
                    p2Button.Click += (s, e) => { selectedPlayer = sevenSwapCardsHandler(currentPlayer, p2Button.Text, cardId); if (sevenCardHandlerInvalidCase == true) playerSwapForm.Close(); };
                    p3Button.Click += (s, e) => { selectedPlayer = sevenSwapCardsHandler(currentPlayer, p3Button.Text, cardId); if (sevenCardHandlerInvalidCase == true) playerSwapForm.Close(); };
                    p4Button.Click += (s, e) => { selectedPlayer = sevenSwapCardsHandler(currentPlayer, p4Button.Text, cardId); if (sevenCardHandlerInvalidCase == true) playerSwapForm.Close(); };
                    // Add the buttons to the form
                    playerSwapForm.Controls.Add(p1Button);
                    playerSwapForm.Controls.Add(p2Button);
                    playerSwapForm.Controls.Add(p3Button);
                    playerSwapForm.Controls.Add(p4Button);
                    // Show the form as a dialog
                    playerSwapForm.ShowDialog();

                }
                if (sevenCardHandlerInvalidCase == true)
                {
                    nextTurnEvent(getPlayerName(currentPlayer)); // Call the nextTurnEvent function with the current player as a parameter to update the game state accordingly, but only if the case is valid
                }
            }
            // Do nothing if the card played is not a Seven card
            else
            {
                return;
            }
        }



        public void zeroCardOrderedDeckSwitchEvent(string currentPlayer, string nextPlayer, string cardId)
        {
            //Initialize the string holders for each player
            string[] currentPlayerHand = new string[25];
            string[] nextPlayerHand = new string[25];
            string[] thirdPlayerHand = new string[25];
            string[] fourthPlayerHand = new string[25];

            //Initialize the card number of each player
            int currentPlayerCardCount = 0;
            int nextPlayerCardCount = 0;
            int thirdPlayerCardCount = 0;
            int fourthPlayerCardCount = 0;


            //This function is called when a player plays a Zero card, 
            if (cardId.Contains("Zero"))
            {
                if (reverseDirection == false) // If the direction of play is not reversed, determine the current player and set the corresponding card count variables for use in swapping hands with the next player based on the player's selection
                {
                    switch (currentPlayer) //ADD THE HANDLERS WHEN YOU GET ON NEXT TIME
                    {
                        case string c when c.Contains(gbP1.Text):
                            // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                            currentPlayerCardCount = cardCountP1;
                            nextPlayerCardCount = cardCountP2;
                            thirdPlayerCardCount = cardCountP3;
                            fourthPlayerCardCount = cardCountP4;

                            for (int i = 0; i < cardCountP1; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the next player based on the player's selection
                            {
                                currentPlayerHand[i] = cardsP1[i];
                                cardsP1[i] = "";
                                cbP1PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP2; i++) // Loop through the next player's hand and store the cards in the nextPlayerHand array, clear the next player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                nextPlayerHand[i] = cardsP2[i];
                                cardsP2[i] = "";
                                cbP2PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP3; i++) // Loop through the third player's hand and store the cards in the thirdPlayerHand array, clear the third player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                thirdPlayerHand[i] = cardsP3[i];
                                cardsP3[i] = "";
                                cbP3PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP4; i++) // Loop through the fourth player's hand and store the cards in the fourthPlayerHand array, clear the fourth player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                fourthPlayerHand[i] = cardsP4[i];
                                cardsP4[i] = "";
                                cbP4PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < currentPlayerCardCount; i++) // Loop through the current player's hand and check if the card played is in the current player's hand, if it is, remove it from the current player's hand by assigning the value of the next card in the array to the current card's position
                            {
                                if (!string.IsNullOrEmpty(currentPlayerHand[i]) && currentPlayerHand[i].Contains(cardId))
                                {
                                    currentPlayerHand[i] = currentPlayerHand[i + 1];
                                }
                            }
                            currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to an empty string to remove the card played from the current player's hand
                            currentPlayerCardCount--; // Decrement the current player's card count by 1 to reflect the removal of the card played from the current player's hand

                            for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the next player by assigning the values from the currentPlayerHand array to the next player's card array and the values from the nextPlayerHand array to the current player's card array based on the player's selection
                            {
                                cardsP2[i] = currentPlayerHand[i];
                                cardsP3[i] = nextPlayerHand[i];
                                cardsP4[i] = thirdPlayerHand[i];
                                cardsP1[i] = fourthPlayerHand[i];
                            }

                            // Update the card count variables for each player to reflect the new card counts after the swap of hands between the current player and the next player based on the player's selection
                            cardCountP1 = fourthPlayerCardCount;
                            cardCountP2 = currentPlayerCardCount;
                            cardCountP3 = currentPlayerCardCount;
                            cardCountP4 = currentPlayerCardCount;

                            for (int i = 0; i < cardCountP1; i++) // Loop through the current player's card array and add the cards to the combo box
                            {
                                if (cardsP1[i] != null)
                                {
                                    cbP1PlayableCard.Items.Add(cardsP1[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP2; i++) // Loop through the next player's card array and add the cards to the combo box
                            {
                                if (cardsP2[i] != null)
                                {
                                    cbP2PlayableCard.Items.Add(cardsP2[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP3; i++) // Loop through the third player's card array and add the cards to the combo box
                            {
                                if (cardsP3[i] != null)
                                {
                                    cbP3PlayableCard.Items.Add(cardsP3[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP4; i++) // Loop through the fourth player's card array and add the cards to the combo box
                            {
                                if (cardsP4[i] != null)
                                {
                                    cbP4PlayableCard.Items.Add(cardsP4[i]);
                                }
                            }

                            break;
                        case string c when c.Contains(gbP2.Text):
                            // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                            currentPlayerCardCount = cardCountP2;
                            nextPlayerCardCount = cardCountP3;
                            thirdPlayerCardCount = cardCountP4;
                            fourthPlayerCardCount = cardCountP1;

                            for (int i = 0; i < cardCountP2; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the next player based on the player's selection
                            {
                                currentPlayerHand[i] = cardsP2[i];
                                cardsP2[i] = "";
                                cbP2PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP3; i++) // Loop through the next player's hand and store the cards in the nextPlayerHand array, clear the next player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                nextPlayerHand[i] = cardsP3[i];
                                cardsP3[i] = "";
                                cbP3PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP4; i++) // Loop through the third player's hand and store the cards in the thirdPlayerHand array, clear the third player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                thirdPlayerHand[i] = cardsP4[i];
                                cardsP4[i] = "";
                                cbP4PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP1; i++) // Loop through the fourth player's hand and store the cards in the fourthPlayerHand array, clear the fourth player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                fourthPlayerHand[i] = cardsP1[i];
                                cardsP1[i] = "";
                                cbP1PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < currentPlayerCardCount; i++)
                            {
                                if (!string.IsNullOrEmpty(currentPlayerHand[i]) && currentPlayerHand[i].Contains(cardId))
                                {
                                    currentPlayerHand[i] = currentPlayerHand[i + 1];
                                }
                            }
                            currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to an empty string to remove the card played from the current player's hand
                            currentPlayerCardCount--; // Decrement the current player's card count by 1 to reflect the removal of the card played from the current player's hand

                            for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the next player by assigning the values from the currentPlayerHand array to the next player's card array and the values from the nextPlayerHand array to the current player's card array based on the player's selection
                            {
                                cardsP3[i] = currentPlayerHand[i];
                                cardsP4[i] = nextPlayerHand[i];
                                cardsP1[i] = thirdPlayerHand[i];
                                cardsP2[i] = fourthPlayerHand[i];
                            }

                            cardCountP2 = fourthPlayerCardCount;
                            cardCountP3 = currentPlayerCardCount;
                            cardCountP4 = nextPlayerCardCount;
                            cardCountP1 = thirdPlayerCardCount;

                            for (int i = 0; i < cardCountP2; i++) // Loop through the current player's card array and add the cards to the combo box
                            {
                                if (cardsP2[i] != null)
                                {
                                    cbP2PlayableCard.Items.Add(cardsP2[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP3; i++) // Loop through the next player's card array and add the cards to the combo box
                            {
                                if (cardsP3[i] != null)
                                {
                                    cbP3PlayableCard.Items.Add(cardsP3[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP4; i++) // Loop through the third player's card array and add the cards to the combo box
                            {
                                if (cardsP4[i] != null)
                                {
                                    cbP4PlayableCard.Items.Add(cardsP4[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP1; i++) // Loop through the fourth player's card array and add the cards to the combo box
                            {
                                if (cardsP1[i] != null)
                                {
                                    cbP1PlayableCard.Items.Add(cardsP1[i]);
                                }
                            }

                            break;
                        case string c when c.Contains(gbP3.Text):
                            // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                            currentPlayerCardCount = cardCountP3;
                            nextPlayerCardCount = cardCountP4;
                            thirdPlayerCardCount = cardCountP1;
                            fourthPlayerCardCount = cardCountP2;

                            for (int i = 0; i < cardCountP3; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the next player based on the player's selection
                            {
                                currentPlayerHand[i] = cardsP3[i];
                                cardsP3[i] = "";
                                cbP3PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP4; i++) // Loop through the next player's hand and store the cards in the nextPlayerHand array, clear the next player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                nextPlayerHand[i] = cardsP4[i];
                                cardsP4[i] = "";
                                cbP4PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP1; i++) // Loop through the third player's hand and store the cards in the thirdPlayerHand array, clear the third player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                thirdPlayerHand[i] = cardsP1[i];
                                cardsP1[i] = "";
                                cbP1PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP2; i++) // Loop through the fourth player's hand and store the cards in the fourthPlayerHand array, clear the fourth player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                fourthPlayerHand[i] = cardsP2[i];
                                cardsP2[i] = "";
                                cbP2PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < currentPlayerCardCount; i++)
                            {
                                if (!string.IsNullOrEmpty(currentPlayerHand[i]) && currentPlayerHand[i].Contains(cardId))
                                {
                                    currentPlayerHand[i] = currentPlayerHand[i + 1];
                                }
                            }
                            currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to an empty string to remove the card played from the current player's hand
                            currentPlayerCardCount--; // Decrement the current player's card count by 1 to reflect the removal of the card played from the current player's hand

                            for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the next player by assigning the values from the currentPlayerHand array to the next player's card array and the values from the nextPlayerHand array to the current player's card array based on the player's selection
                            {
                                cardsP4[i] = currentPlayerHand[i];
                                cardsP1[i] = nextPlayerHand[i];
                                cardsP2[i] = thirdPlayerHand[i];
                                cardsP3[i] = fourthPlayerHand[i];
                            }

                            // Update the card count variables for each player to reflect the new card counts after the swap of hands between the current player and the next player based on the player's selection
                            cardCountP3 = fourthPlayerCardCount;
                            cardCountP4 = currentPlayerCardCount;
                            cardCountP1 = nextPlayerCardCount;
                            cardCountP2 = thirdPlayerCardCount;

                            for (int i = 0; i < cardCountP3; i++) // Loop through the current player's card array and add the cards to the combo box
                            {
                                if (cardsP3[i] != null)
                                {
                                    cbP3PlayableCard.Items.Add(cardsP3[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP4; i++) // Loop through the next player's card array and add the cards to the combo box
                            {
                                if (cardsP4[i] != null)
                                {
                                    cbP4PlayableCard.Items.Add(cardsP4[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP1; i++) // Loop through the third player's card array and add the cards to the combo box
                            {
                                if (cardsP1[i] != null)
                                {
                                    cbP1PlayableCard.Items.Add(cardsP1[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP2; i++) // Loop through the fourth player's card array and add the cards to the combo box
                            {
                                if (cardsP2[i] != null)
                                {
                                    cbP2PlayableCard.Items.Add(cardsP2[i]);
                                }
                            }

                            break;
                        case string c when c.Contains(gbP4.Text):
                            // Set the current player's card count to the corresponding variable based on the playerPlayed parameter for use in swapping hands with the chosen player based on the player's selection
                            currentPlayerCardCount = cardCountP4;
                            nextPlayerCardCount = cardCountP1;
                            thirdPlayerCardCount = cardCountP2;
                            fourthPlayerCardCount = cardCountP3;

                            for (int i = 0; i < cardCountP4; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the next player based on the player's selection
                            {
                                currentPlayerHand[i] = cardsP4[i];
                                cardsP4[i] = "";
                                cbP4PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP1; i++) // Loop through the next player's hand and store the cards in the nextPlayerHand array, clear the next player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                nextPlayerHand[i] = cardsP1[i];
                                cardsP1[i] = "";
                                cbP1PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP2; i++) // Loop through the third player's hand and store the cards in the thirdPlayerHand array, clear the third player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                thirdPlayerHand[i] = cardsP2[i];
                                cardsP2[i] = "";
                                cbP2PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP3; i++) // Loop through the fourth player's hand and store the cards in the fourthPlayerHand array, clear the fourth player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                fourthPlayerHand[i] = cardsP3[i];
                                cardsP3[i] = "";
                                cbP3PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < currentPlayerCardCount; i++)
                            {
                                if (!string.IsNullOrEmpty(currentPlayerHand[i]) && currentPlayerHand[i].Contains(cardId))
                                {
                                    currentPlayerHand[i] = currentPlayerHand[i + 1];
                                }
                            }
                            currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to an empty string to remove the card played from the current player's hand
                            currentPlayerCardCount--; // Decrement the current player's card count by 1 to reflect the removal of the card played from the current player's hand

                            for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the next player by assigning the values from the currentPlayerHand array to the next player's card array and the values from the nextPlayerHand array to the current player's card array based on the player's selection
                            {
                                cardsP1[i] = currentPlayerHand[i];
                                cardsP2[i] = nextPlayerHand[i];
                                cardsP3[i] = thirdPlayerHand[i];
                                cardsP4[i] = fourthPlayerHand[i];
                            }

                            // Update the card count variables for each player to reflect the new card counts after the swap of hands between the current player and the next player based on the player's selection
                            cardCountP4 = fourthPlayerCardCount;
                            cardCountP1 = currentPlayerCardCount;
                            cardCountP2 = nextPlayerCardCount;
                            cardCountP3 = thirdPlayerCardCount;

                            for (int i = 0; i < cardCountP4; i++) // Loop through the current player's card array and add the cards to the combo box
                            {
                                if (cardsP4[i] != null)
                                {
                                    cbP4PlayableCard.Items.Add(cardsP4[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP1; i++) // Loop through the next player's card array and add the cards to the combo box
                            {
                                if (cardsP1[i] != null)
                                {
                                    cbP1PlayableCard.Items.Add(cardsP1[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP2; i++) // Loop through the third player's card array and add the cards to the combo box
                            {
                                if (cardsP2[i] != null)
                                {
                                    cbP2PlayableCard.Items.Add(cardsP2[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP3; i++) // Loop through the fourth player's card array and add the cards to the combo box
                            {
                                if (cardsP3[i] != null)
                                {
                                    cbP3PlayableCard.Items.Add(cardsP3[i]);
                                }
                            }

                            break;
                    }
                }
                else if (reverseDirection == true)
                {
                    switch (currentPlayer)
                    {
                        case string c when c.Contains(gbP1.Text):

                            currentPlayerCardCount = cardCountP1;
                            nextPlayerCardCount = cardCountP4;
                            thirdPlayerCardCount = cardCountP3;
                            fourthPlayerCardCount = cardCountP2;

                            for (int i = 0; i < cardCountP1; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the next player based on the player's selection
                            {
                                currentPlayerHand[i] = cardsP1[i];
                                cardsP1[i] = "";
                                cbP1PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP4; i++) // Loop through the next player's hand and store the cards in the nextPlayerHand array, clear the next player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                nextPlayerHand[i] = cardsP4[i];
                                cardsP4[i] = "";
                                cbP4PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP3; i++) // Loop through the third player's hand and store the cards in the thirdPlayerHand array, clear the third player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                thirdPlayerHand[i] = cardsP3[i];
                                cardsP3[i] = "";
                                cbP3PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP2; i++) // Loop through the fourth player's hand and store the cards in the fourthPlayerHand array, clear the fourth player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                fourthPlayerHand[i] = cardsP2[i];
                                cardsP2[i] = "";
                                cbP2PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < currentPlayerCardCount; i++)
                            {
                                if (!string.IsNullOrEmpty(currentPlayerHand[i]) && currentPlayerHand[i].Contains(cardId))
                                {
                                    currentPlayerHand[i] = currentPlayerHand[i + 1];
                                }
                            }
                            currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to an empty string to remove the card played from the current player's hand
                            currentPlayerCardCount--; // Decrement the current player's card count by 1 to reflect the removal of the card played from the current player's hand

                            for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the next player by assigning the values from the currentPlayerHand array to the next player's card array and the values from the nextPlayerHand array to the current player's card array based on the player's selection
                            {
                                cardsP4[i] = currentPlayerHand[i];
                                cardsP3[i] = nextPlayerHand[i];
                                cardsP2[i] = thirdPlayerHand[i];
                                cardsP1[i] = fourthPlayerHand[i];
                            }

                            // Update the card count variables for each player to reflect the new card counts after the swap of hands between the current player and the next player based on the player's selection
                            cardCountP1 = fourthPlayerCardCount;
                            cardCountP4 = currentPlayerCardCount;
                            cardCountP3 = nextPlayerCardCount;
                            cardCountP2 = thirdPlayerCardCount;

                            for (int i = 0; i < cardCountP1; i++) // Loop through the current player's card array and add the cards to the combo box
                            {
                                if (cardsP1[i] != null)
                                {
                                    cbP1PlayableCard.Items.Add(cardsP1[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP4; i++) // Loop through the next player's card array and add the cards to the combo box
                            {
                                if (cardsP4[i] != null)
                                {
                                    cbP4PlayableCard.Items.Add(cardsP4[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP3; i++) // Loop through the third player's card array and add the cards to the combo box
                            {
                                if (cardsP3[i] != null)
                                {
                                    cbP3PlayableCard.Items.Add(cardsP3[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP2; i++) // Loop through the fourth player's card array and add the cards to the combo box
                            {
                                if (cardsP2[i] != null)
                                {
                                    cbP2PlayableCard.Items.Add(cardsP2[i]);
                                }
                            }

                            break;
                        case string c when c.Contains(gbP2.Text):
                            // Initialize the card count variables for each player to reflect the new card counts after the swap of hands between the current player and the next player based on the player's selection
                            currentPlayerCardCount = cardCountP2;
                            nextPlayerCardCount = cardCountP1;
                            thirdPlayerCardCount = cardCountP4;
                            fourthPlayerCardCount = cardCountP3;

                            for (int i = 0; i < cardCountP2; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the next player based on the player's selection
                            {
                                currentPlayerHand[i] = cardsP2[i];
                                cardsP2[i] = "";
                                cbP2PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP1; i++) // Loop through the next player's hand and store the cards in the nextPlayerHand array, clear the next player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                nextPlayerHand[i] = cardsP1[i];
                                cardsP1[i] = "";
                                cbP1PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP4; i++) // Loop through the third player's hand and store the cards in the thirdPlayerHand array, clear the third player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                thirdPlayerHand[i] = cardsP4[i];
                                cardsP4[i] = "";
                                cbP4PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP3; i++) // Loop through the fourth player's hand and store the cards in the fourthPlayerHand array, clear the fourth player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                fourthPlayerHand[i] = cardsP3[i];
                                cardsP3[i] = "";
                                cbP3PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < currentPlayerCardCount; i++) // Loop through the current player's hand and check if the card played is in the current player's hand, if it is, remove it from the current player's hand by assigning the value of the next card in the array to the current card's position
                            {
                                if (!string.IsNullOrEmpty(currentPlayerHand[i]) && currentPlayerHand[i].Contains(cardId))
                                {
                                    currentPlayerHand[i] = currentPlayerHand[i + 1];
                                }
                            }
                            currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to an empty string to remove the card played from the current player's hand
                            currentPlayerCardCount--; // Decrement the current player's card count by 1 to reflect the removal of the card played from the current player's hand

                            for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the next player by assigning the values from the currentPlayerHand array to the next player's card array and the values from the nextPlayerHand array to the current player's card array based on the player's selection
                            {
                                cardsP1[i] = currentPlayerHand[i];
                                cardsP4[i] = nextPlayerHand[i];
                                cardsP3[i] = thirdPlayerHand[i];
                                cardsP2[i] = fourthPlayerHand[i];
                            }

                            // Reset each players card count to reflect the new card counts after the swap of hands between the current player and the next player based on the player's selection
                            cardCountP2 = fourthPlayerCardCount;
                            cardCountP1 = currentPlayerCardCount;
                            cardCountP4 = nextPlayerCardCount;
                            cardCountP3 = thirdPlayerCardCount;

                            for (int i = 0; i < cardCountP2; i++) // Loop through the current player's card array and add the cards to the combo box
                            {
                                if (cardsP2[i] != null)
                                {
                                    cbP2PlayableCard.Items.Add(cardsP2[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP1; i++) // Loop through the next player's card array and add the cards to the combo box
                            {
                                if (cardsP1[i] != null)
                                {
                                    cbP1PlayableCard.Items.Add(cardsP1[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP4; i++) // Loop through the third player's card array and add the cards to the combo box
                            {
                                if (cardsP4[i] != null)
                                {
                                    cbP4PlayableCard.Items.Add(cardsP4[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP3; i++) // Loop through the fourth player's card array and add the cards to the combo box
                            {
                                if (cardsP3[i] != null)
                                {
                                    cbP3PlayableCard.Items.Add(cardsP3[i]);
                                }
                            }

                            break;
                        case string c when c.Contains(gbP3.Text):

                            // Initialize the card count variables for each player to reflect the new card counts after the swap of hands between the current player and the next player based on the player's selection
                            currentPlayerCardCount = cardCountP3;
                            nextPlayerCardCount = cardCountP2;
                            thirdPlayerCardCount = cardCountP1;
                            fourthPlayerCardCount = cardCountP4;

                            for (int i = 0; i < cardCountP3; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the next player based on the player's selection
                            {
                                currentPlayerHand[i] = cardsP3[i];
                                cardsP3[i] = "";
                                cbP3PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP2; i++) // Loop through the next player's hand and store the cards in the nextPlayerHand array, clear the next player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                nextPlayerHand[i] = cardsP2[i];
                                cardsP2[i] = "";
                                cbP2PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP1; i++) // Loop through the third player's hand and store the cards in the thirdPlayerHand array, clear the third player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                thirdPlayerHand[i] = cardsP1[i];
                                cardsP1[i] = "";
                                cbP1PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP4; i++) // Loop through the fourth player's hand and store the cards in the fourthPlayerHand array, clear the fourth player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                fourthPlayerHand[i] = cardsP4[i];
                                cardsP4[i] = "";
                                cbP4PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < currentPlayerCardCount; i++)
                            {
                                if (!string.IsNullOrEmpty(currentPlayerHand[i]) && currentPlayerHand[i].Contains(cardId))
                                {
                                    currentPlayerHand[i] = currentPlayerHand[i + 1];
                                }
                            }
                            currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to an empty string to remove the card played from the current player's hand
                            currentPlayerCardCount--; // Decrement the current player's card count by 1 to reflect the removal of the card played from the current player's hand

                            for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the next player by assigning the values from the currentPlayerHand array to the next player's card array and the values from the nextPlayerHand array to the current player's card array based on the player's selection
                            {
                                cardsP2[i] = currentPlayerHand[i];
                                cardsP1[i] = nextPlayerHand[i];
                                cardsP4[i] = thirdPlayerHand[i];
                                cardsP3[i] = fourthPlayerHand[i];
                            }

                            // Reset each players card count to reflect the new card counts after the swap of hands between the current player and the next player based on the player's selection
                            cardCountP3 = fourthPlayerCardCount;
                            cardCountP2 = currentPlayerCardCount;
                            cardCountP1 = nextPlayerCardCount;
                            cardCountP4 = thirdPlayerCardCount;

                            for (int i = 0; i < cardCountP3; i++) // Loop through the current player's card array and add the cards to the combo box
                            {
                                if (cardsP3[i] != null)
                                {
                                    cbP3PlayableCard.Items.Add(cardsP3[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP2; i++) // Loop through the next player's card array and add the cards to the combo box
                            {
                                if (cardsP2[i] != null)
                                {
                                    cbP2PlayableCard.Items.Add(cardsP2[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP1; i++) // Loop through the third player's card array and add the cards to the combo box
                            {
                                if (cardsP1[i] != null)
                                {
                                    cbP1PlayableCard.Items.Add(cardsP1[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP4; i++) // Loop through the fourth player's card array and add the cards to the combo box
                            {
                                if (cardsP4[i] != null)
                                {
                                    cbP4PlayableCard.Items.Add(cardsP4[i]);
                                }
                            }

                            break;
                        case string c when c.Contains(gbP4.Text):
                            // Initialize the card count variables for each player to reflect the new card counts after the swap of hands between the current player and the next player based on the player's selection
                            currentPlayerCardCount = cardCountP4;
                            nextPlayerCardCount = cardCountP3;
                            thirdPlayerCardCount = cardCountP2;
                            fourthPlayerCardCount = cardCountP1;

                            for (int i = 0; i < cardCountP4; i++) // Loop through the current player's hand and store the cards in the currentPlayerHand array, clear the current player's card array and combo box items to prepare for swapping hands with the next player based on the player's selection
                            {
                                currentPlayerHand[i] = cardsP4[i];
                                cardsP4[i] = "";
                                cbP4PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP3; i++) // Loop through the next player's hand and store the cards in the nextPlayerHand array, clear the next player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                nextPlayerHand[i] = cardsP3[i];
                                cardsP3[i] = "";
                                cbP3PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP2; i++) // Loop through the third player's hand and store the cards in the thirdPlayerHand array, clear the third player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                thirdPlayerHand[i] = cardsP2[i];
                                cardsP2[i] = "";
                                cbP2PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < cardCountP1; i++) // Loop through the fourth player's hand and store the cards in the fourthPlayerHand array, clear the fourth player's card array and combo box items to prepare for swapping hands with the current player based on the player's selection
                            {
                                fourthPlayerHand[i] = cardsP1[i];
                                cardsP1[i] = "";
                                cbP1PlayableCard.Items.Clear();
                            }

                            for (int i = 0; i < currentPlayerCardCount; i++)
                            {
                                if (!string.IsNullOrEmpty(currentPlayerHand[i]) && currentPlayerHand[i].Contains(cardId))
                                {
                                    currentPlayerHand[i] = currentPlayerHand[i + 1];
                                }
                            }
                            currentPlayerHand[currentPlayerCardCount] = ""; // Set the last card in the current player's hand to an empty string to remove the card played from the current player's hand
                            currentPlayerCardCount--; // Decrement the current player's card count by 1 to reflect the removal of the card played from the current player's hand

                            for (int i = 0; i < 25; i++) // Loop through the card arrays and swap the cards between the current player and the next player by assigning the values from the currentPlayerHand array to the next player's card array and the values from the nextPlayerHand array to the current player's card array based on the player's selection
                            {
                                cardsP3[i] = currentPlayerHand[i];
                                cardsP2[i] = nextPlayerHand[i];
                                cardsP1[i] = thirdPlayerHand[i];
                                cardsP4[i] = fourthPlayerHand[i];
                            }

                            // Reset each players card count to reflect the new card counts after the swap of hands between the current player and the next player based on the player's selection
                            cardCountP4 = fourthPlayerCardCount;
                            cardCountP3 = currentPlayerCardCount;
                            cardCountP2 = nextPlayerCardCount;
                            cardCountP1 = thirdPlayerCardCount;

                            for (int i = 0; i < cardCountP4; i++) // Loop through the current player's card array and add the cards to the combo box
                            {
                                if (cardsP4[i] != null)
                                {
                                    cbP4PlayableCard.Items.Add(cardsP4[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP3; i++) // Loop through the next player's card array and add the cards to the combo box
                            {
                                if (cardsP3[i] != null)
                                {
                                    cbP3PlayableCard.Items.Add(cardsP3[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP2; i++) // Loop through the third player's card array and add the cards to the combo box
                            {
                                if (cardsP2[i] != null)
                                {
                                    cbP2PlayableCard.Items.Add(cardsP2[i]);
                                }
                            }

                            for (int i = 0; i < cardCountP1; i++) // Loop through the fourth player's card array and add the cards to the combo box
                            {
                                if (cardsP1[i] != null)
                                {
                                    cbP1PlayableCard.Items.Add(cardsP1[i]);
                                }
                            }
                            break;
                    }
                }
                // Display a message box to indicate that all players have swapped hands in the order of play
                MessageBox.Show("All players have swapped hands in the order of play!", "Zero Card Swap", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nextTurnEvent(getPlayerName(currentPlayer)); // Call the nextTurnEvent function with the current player as a parameter to update the game state accordingly
            }
            // Do nothing if the card played is not a Zero card
            else
            {
                return;
            }
        }

        //This function handles the logic for when a player plays a card, it checks if the card played is valid and updates the game state accordingly
        public bool cardHandler(string cardId, string nextPlayer, string currentPlayer)
        {
            // If the card is a wild card (contains "6", "10", "Roulette", or "Switch"), call the corresponding functions to update the game state based on the type of wild card played and return true to indicate that the card played is valid and the game state has been updated accordingly
            if (cardId.Contains("6") || cardId.Contains("10") || cardId.Contains("Roulette") || cardId.Contains("Switch"))
            {
                // If the card played is a wild card, call the corresponding functions to update the game state based on the type of wild card played
                reversePickupFourEvent(currentPlayer, cardId);
                pickupSixEvent(currentPlayer, nextPlayer, cardId);
                pickupTenEvent(currentPlayer, nextPlayer, cardId);
                rouletteCardEvent(currentPlayer, nextPlayer, cardId);
                return true; // Return true if the card played is a valid wild card and the game state has been updated accordingly
            }

            // Check if the card played is valid by checking if it is the same color as the previous card played or if it is a wild card, and return true if it is valid, otherwise return false
            if (!(cardId.Contains("6") || cardId.Contains("10") || cardId.Contains("Roulette") || cardId.Contains("Switch")))
            {
                if (sameColorCheck(cardId) || sameValueCheck(cardId) || sameSpecialColoredCardCheck(cardId))
                {
                    // If the card played is valid, call the corresponding functions to update the game state based on the type of card played
                    skipAllEvent(cardId);
                    skipNextEvent(currentPlayer, cardId);
                    numberCardEvent(currentPlayer, cardId);
                    reverseCardEvent(currentPlayer, cardId);
                    discardCardEvent(currentPlayer, nextPlayer, cardId);
                    pickupTwoEvent(currentPlayer, nextPlayer, cardId);
                    pickupFourEvent(currentPlayer, nextPlayer, cardId);
                    sevenCardChosenSwapEvent(currentPlayer, nextPlayer, cardId);
                    zeroCardOrderedDeckSwitchEvent(currentPlayer, nextPlayer, cardId);

                    return true; // Return true if the card played is valid and the game state has been updated accordingly
                }
            }

            return false;
        }

        // This function is called at the end of each turn to update the game state and enable the controls for the next player while disabling the controls for the other players
        // The seperate messageboxes are there because the order of the players is different depending on the current direction of play, so the controls are enabled accordingly and a message box is displayed to indicate which player's turn it is next
        public void nextTurnEvent(string nextPlayer)
        {
            // Check the next player and enable the controls for that player while disabling the controls for the other players based on the current direction of play
            if (nextPlayer == gbP2.Text) // Disables P1 and enables nextPlayer according to the cases
            {
                if (p2Lost == false)
                {
                    // If Player 2 has not lost the game, enable the controls for Player 2 
                    cbP2PlayableCard.Enabled = true;
                    btnP2Play.Enabled = true;
                    btnP2UNO.Enabled = true;

                    // Disable the controls for the other players
                    cbP1PlayableCard.Enabled = false;
                    btnP1Play.Enabled = false;
                    btnP1UNO.Enabled = false;
                    cbP3PlayableCard.Enabled = false;
                    btnP3Play.Enabled = false;
                    btnP3UNO.Enabled = false;
                    cbP4PlayableCard.Enabled = false;
                    btnP4Play.Enabled = false;
                    btnP4UNO.Enabled = false;

                    // Display a message box to indicate which player's turn it is next
                    MessageBox.Show(gbP2.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Change the background color of the player group boxes to indicate which player's turn it is next
                    p1bgColor = Color.Crimson;
                    p2bgColor = Color.Red;
                    p3bgColor = Color.Crimson;
                    p4bgColor = Color.Crimson;

                }
                else if (p2Lost == true && p3Lost == true || p2Lost == true && p1Lost == true)
                {
                    // If Player 2 and 3 has lost the game, enable the controls for Player 4 
                    cbP4PlayableCard.Enabled = true;
                    btnP4Play.Enabled = true;
                    btnP4UNO.Enabled = true;

                    // Disable the controls for the other players
                    cbP1PlayableCard.Enabled = false;
                    btnP1Play.Enabled = false;
                    btnP1UNO.Enabled = false;
                    cbP2PlayableCard.Enabled = false;
                    btnP2Play.Enabled = false;
                    btnP2UNO.Enabled = false;
                    cbP3PlayableCard.Enabled = false;
                    btnP3Play.Enabled = false;
                    btnP3UNO.Enabled = false;

                    // Display a message box to indicate which player's turn it is next
                    MessageBox.Show(gbP4.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Change the background color of the player group boxes to indicate which player's turn it is next
                    p1bgColor = Color.Crimson;
                    p2bgColor = Color.Crimson;
                    p3bgColor = Color.Crimson;
                    p4bgColor = Color.Red;
                }
                else
                {
                    if (!reverseDirection)
                    {
                        // If Player 2 has lost the game, enable the controls for Player 3 
                        cbP3PlayableCard.Enabled = true;
                        btnP3Play.Enabled = true;
                        btnP3UNO.Enabled = true;

                        // Disable the controls for the other players
                        cbP1PlayableCard.Enabled = false;
                        btnP1Play.Enabled = false;
                        btnP1UNO.Enabled = false;
                        cbP2PlayableCard.Enabled = false;
                        btnP2Play.Enabled = false;
                        btnP2UNO.Enabled = false;
                        cbP4PlayableCard.Enabled = false;
                        btnP4Play.Enabled = false;
                        btnP4UNO.Enabled = false;

                        // Display a message box to indicate which player's turn it is next
                        MessageBox.Show(gbP3.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Change the background color of the player group boxes to indicate which player's turn it is next
                        p1bgColor = Color.Crimson;
                        p2bgColor = Color.Crimson;
                        p3bgColor = Color.Red;
                        p4bgColor = Color.Crimson;
                    }
                    else
                    {
                        // If Player 2 has lost the game, enable the controls for Player 1 
                        cbP1PlayableCard.Enabled = true;
                        btnP1Play.Enabled = true;
                        btnP1UNO.Enabled = true;

                        // Disable the controls for the other players
                        cbP2PlayableCard.Enabled = false;
                        btnP2Play.Enabled = false;
                        btnP2UNO.Enabled = false;
                        cbP3PlayableCard.Enabled = false;
                        btnP3Play.Enabled = false;
                        btnP3UNO.Enabled = false;
                        cbP4PlayableCard.Enabled = false;
                        btnP4Play.Enabled = false;
                        btnP4UNO.Enabled = false;

                        // Display a message box to indicate which player's turn it is next
                        MessageBox.Show(gbP1.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Change the background color of the player group boxes to indicate which player's turn it is next
                        p1bgColor = Color.Red;
                        p2bgColor = Color.Crimson;
                        p3bgColor = Color.Crimson;
                        p4bgColor = Color.Crimson;
                    }
                }
            }
            else if (nextPlayer == gbP3.Text) // Disables P2 and enables nextPlayer according to the cases
            {
                if (p3Lost == false)
                {
                    // If Player 3 has not lost the game enable the controls for Player 3
                    cbP3PlayableCard.Enabled = true;
                    btnP3Play.Enabled = true;
                    btnP3UNO.Enabled = true;

                    // Disable the controls for the other players
                    cbP1PlayableCard.Enabled = false;
                    btnP1Play.Enabled = false;
                    btnP1UNO.Enabled = false;
                    cbP2PlayableCard.Enabled = false;
                    btnP2Play.Enabled = false;
                    btnP2UNO.Enabled = false;
                    cbP4PlayableCard.Enabled = false;
                    btnP4Play.Enabled = false;
                    btnP4UNO.Enabled = false;

                    // Display a message box to indicate which player's turn it is next
                    MessageBox.Show(gbP3.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Change the background color of the player group boxes to indicate which player's turn it is next
                    p1bgColor = Color.Crimson;
                    p2bgColor = Color.Crimson;
                    p3bgColor = Color.Red;
                    p4bgColor = Color.Crimson;
                }
                else if (p3Lost == true && p4Lost == true || p3Lost == true && p2Lost == true)
                {
                    // If Player 3 and Player 4 have lost the game, enable the controls for Player 2 
                    cbP1PlayableCard.Enabled = true;
                    btnP1Play.Enabled = true;
                    btnP1UNO.Enabled = true;

                    // Disable the controls for the other players
                    cbP2PlayableCard.Enabled = false;
                    btnP2Play.Enabled = false;
                    btnP2UNO.Enabled = false;
                    cbP3PlayableCard.Enabled = false;
                    btnP3Play.Enabled = false;
                    btnP3UNO.Enabled = false;
                    cbP4PlayableCard.Enabled = false;
                    btnP4Play.Enabled = false;
                    btnP4UNO.Enabled = false;


                    // Display a message box to indicate which player's turn it is next
                    MessageBox.Show(gbP1.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Change the background color of the player group boxes to indicate which player's turn it is next
                    p1bgColor = Color.Red;
                    p2bgColor = Color.Crimson;
                    p3bgColor = Color.Crimson;
                    p4bgColor = Color.Crimson;
                }
                else
                {
                    if (!reverseDirection)
                    {
                        // If Player 3 has lost the game and Player 4 has not lost the game, enable the controls for Player 4
                        cbP4PlayableCard.Enabled = true;
                        btnP4Play.Enabled = true;
                        btnP4UNO.Enabled = true;

                        // Disable the controls for the other players
                        cbP1PlayableCard.Enabled = false;
                        btnP1Play.Enabled = false;
                        btnP1UNO.Enabled = false;
                        cbP2PlayableCard.Enabled = false;
                        btnP2Play.Enabled = false;
                        btnP2UNO.Enabled = false;
                        cbP3PlayableCard.Enabled = false;
                        btnP3Play.Enabled = false;
                        btnP3UNO.Enabled = false;

                        // Display a message box to indicate which player's turn it is next
                        MessageBox.Show(gbP4.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // If Player 3 has lost the game and Player 4 has not lost the game, enable the controls for Player 4
                        cbP2PlayableCard.Enabled = true;
                        btnP2Play.Enabled = true;
                        btnP2UNO.Enabled = true;

                        // Disable the controls for the other players
                        cbP1PlayableCard.Enabled = false;
                        btnP1Play.Enabled = false;
                        btnP1UNO.Enabled = false;
                        cbP4PlayableCard.Enabled = false;
                        btnP4Play.Enabled = false;
                        btnP4UNO.Enabled = false;
                        cbP3PlayableCard.Enabled = false;
                        btnP3Play.Enabled = false;
                        btnP3UNO.Enabled = false;

                        // Display a message box to indicate which player's turn it is next
                        MessageBox.Show(gbP2.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Change the background color of the player group boxes to indicate which player's turn it is next
                        p1bgColor = Color.Crimson;
                        p2bgColor = Color.Red;
                        p3bgColor = Color.Crimson;
                        p4bgColor = Color.Crimson;
                    }
                }
            }
            else if (nextPlayer == gbP4.Text) // Disables P3 and enables nextPlayer according to the cases
            {
                if (p4Lost == false)
                {
                    // If Player 4 has not lost the game, enable the controls for Player 4 
                    cbP4PlayableCard.Enabled = true;
                    btnP4Play.Enabled = true;
                    btnP4UNO.Enabled = true;

                    // Disable the controls for the other players
                    cbP1PlayableCard.Enabled = false;
                    btnP1Play.Enabled = false;
                    btnP1UNO.Enabled = false;
                    cbP2PlayableCard.Enabled = false;
                    btnP2Play.Enabled = false;
                    btnP2UNO.Enabled = false;
                    cbP3PlayableCard.Enabled = false;
                    btnP3Play.Enabled = false;
                    btnP3UNO.Enabled = false;

                    // Display a message box to indicate which player's turn it is next
                    MessageBox.Show(gbP4.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Change the background color of the player group boxes to indicate which player's turn it is next
                    p1bgColor = Color.Crimson;
                    p2bgColor = Color.Crimson;
                    p3bgColor = Color.Crimson;
                    p4bgColor = Color.Red;
                }
                else if (p4Lost == true && p1Lost == true || p4Lost == true && p3Lost == true)
                {
                    // If Player 4 has lost the game and Player 1 has lost the game, enable the controls for Player 2
                    cbP2PlayableCard.Enabled = true;
                    btnP2Play.Enabled = true;
                    btnP2UNO.Enabled = true;

                    // Disable the controls for the other players
                    cbP1PlayableCard.Enabled = false;
                    btnP1Play.Enabled = false;
                    btnP1UNO.Enabled = false;
                    cbP3PlayableCard.Enabled = false;
                    btnP3Play.Enabled = false;
                    btnP3UNO.Enabled = false;
                    cbP4PlayableCard.Enabled = false;
                    btnP4Play.Enabled = false;
                    btnP4UNO.Enabled = false;

                    // Display a message box to indicate which player's turn it is next
                    MessageBox.Show(gbP2.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Change the background color of the player group boxes to indicate which player's turn it is next
                    p1bgColor = Color.Crimson;
                    p2bgColor = Color.Red;
                    p3bgColor = Color.Crimson;
                    p4bgColor = Color.Crimson;
                }
                else
                {
                    if (!reverseDirection)
                    {
                        // If Player 4 has lost the game and Player 1 has not lost the game, enable the controls for Player 1
                        cbP1PlayableCard.Enabled = true;
                        btnP1Play.Enabled = true;
                        btnP1UNO.Enabled = true;

                        // Disable the controls for the other players
                        cbP2PlayableCard.Enabled = false;
                        btnP2Play.Enabled = false;
                        btnP2UNO.Enabled = false;
                        cbP3PlayableCard.Enabled = false;
                        btnP3Play.Enabled = false;
                        btnP3UNO.Enabled = false;
                        cbP4PlayableCard.Enabled = false;
                        btnP4Play.Enabled = false;
                        btnP4UNO.Enabled = false;

                        // Display a message box to indicate which player's turn it is next
                        MessageBox.Show(gbP1.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Change the background color of the player group boxes to indicate which player's turn it is next
                        p1bgColor = Color.Red;
                        p2bgColor = Color.Crimson;
                        p3bgColor = Color.Crimson;
                        p4bgColor = Color.Crimson;
                    }
                    else
                    {
                        // If Player 4 has lost the game and Player 1 has not lost the game, enable the controls for Player 3
                        cbP3PlayableCard.Enabled = true;
                        btnP3Play.Enabled = true;
                        btnP3UNO.Enabled = true;

                        // Disable the controls for the other players
                        cbP1PlayableCard.Enabled = false;
                        btnP1Play.Enabled = false;
                        btnP1UNO.Enabled = false;
                        cbP2PlayableCard.Enabled = false;
                        btnP2Play.Enabled = false;
                        btnP2UNO.Enabled = false;
                        cbP4PlayableCard.Enabled = false;
                        btnP4Play.Enabled = false;
                        btnP4UNO.Enabled = false;

                        // Display a message box to indicate which player's turn it is next
                        MessageBox.Show(gbP3.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Change the background color of the player group boxes to indicate which player's turn it is next
                        p1bgColor = Color.Crimson;
                        p2bgColor = Color.Crimson;
                        p3bgColor = Color.Red;
                        p4bgColor = Color.Crimson;
                    }
                }
            }
            else if (nextPlayer == gbP1.Text) // Disables P4 and enables nextPlayer according to the cases
            {
                if (p1Lost == false)
                {
                    // If Player 1 has not lost the game, enable the controls for Player 1
                    cbP1PlayableCard.Enabled = true;
                    btnP1Play.Enabled = true;
                    btnP1UNO.Enabled = true;

                    // Disable the controls for the other players
                    cbP2PlayableCard.Enabled = false;
                    btnP2Play.Enabled = false;
                    btnP2UNO.Enabled = false;
                    cbP3PlayableCard.Enabled = false;
                    btnP3Play.Enabled = false;
                    btnP3UNO.Enabled = false;
                    cbP4PlayableCard.Enabled = false;
                    btnP4Play.Enabled = false;
                    btnP4UNO.Enabled = false;

                    // Display a message box to indicate which player's turn it is next
                    MessageBox.Show(gbP1.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Change the background color of the player group boxes to indicate which player's turn it is next
                    p1bgColor = Color.Red;
                    p2bgColor = Color.Crimson;
                    p3bgColor = Color.Crimson;
                    p4bgColor = Color.Crimson;
                }
                else if (p1Lost == true && p2Lost == true || p1Lost == true && p4Lost == true)
                {
                    // If Player 1 has lost the game and Player 2 has lost the game, enable the controls for Player 3
                    cbP3PlayableCard.Enabled = true;
                    btnP3Play.Enabled = true;
                    btnP3UNO.Enabled = true;

                    // Disable the controls for the other players
                    cbP1PlayableCard.Enabled = false;
                    btnP1Play.Enabled = false;
                    btnP1UNO.Enabled = false;
                    cbP2PlayableCard.Enabled = false;
                    btnP2Play.Enabled = false;
                    btnP2UNO.Enabled = false;
                    cbP4PlayableCard.Enabled = false;
                    btnP4Play.Enabled = false;
                    btnP4UNO.Enabled = false;

                    // Display a message box to indicate which player's turn it is next
                    MessageBox.Show(gbP3.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Change the background color of the player group boxes to indicate which player's turn it is next
                    p1bgColor = Color.Crimson;
                    p2bgColor = Color.Crimson;
                    p3bgColor = Color.Red;
                    p4bgColor = Color.Crimson;
                }
                else
                {
                    if (!reverseDirection)
                    {
                        // If Player 1 has lost the game and Player 2 has not lost the game, enable the controls for Player 2
                        cbP2PlayableCard.Enabled = true;
                        btnP2Play.Enabled = true;
                        btnP2UNO.Enabled = true;

                        // Disable the controls for the other players
                        cbP1PlayableCard.Enabled = false;
                        btnP1Play.Enabled = false;
                        btnP1UNO.Enabled = false;
                        cbP3PlayableCard.Enabled = false;
                        btnP3Play.Enabled = false;
                        btnP3UNO.Enabled = false;
                        cbP4PlayableCard.Enabled = false;
                        btnP4Play.Enabled = false;
                        btnP4UNO.Enabled = false;

                        // Display a message box to indicate which player's turn it is next
                        MessageBox.Show(gbP2.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Change the background color of the player group boxes to indicate which player's turn it is next
                        p1bgColor = Color.Crimson;
                        p2bgColor = Color.Red;
                        p3bgColor = Color.Crimson;
                        p4bgColor = Color.Crimson;
                    }
                    else
                    {
                        // If Player 1 has lost the game and Player 2 has not lost the game, enable the controls for Player 4
                        cbP4PlayableCard.Enabled = true;
                        btnP4Play.Enabled = true;
                        btnP4UNO.Enabled = true;

                        // Disable the controls for the other players
                        cbP1PlayableCard.Enabled = false;
                        btnP1Play.Enabled = false;
                        btnP1UNO.Enabled = false;
                        cbP2PlayableCard.Enabled = false;
                        btnP2Play.Enabled = false;
                        btnP2UNO.Enabled = false;
                        cbP3PlayableCard.Enabled = false;
                        btnP3Play.Enabled = false;
                        btnP3UNO.Enabled = false;

                        // Display a message box to indicate which player's turn it is next
                        MessageBox.Show(gbP4.Text + "'s Turn", "Next Turn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Change the background color of the player group boxes to indicate which player's turn it is next
                        p1bgColor = Color.Crimson;
                        p2bgColor = Color.Crimson;
                        p3bgColor = Color.Crimson;
                        p4bgColor = Color.Red;
                    }
                }
            }
            gbP1.Invalidate();
            gbP2.Invalidate();
            gbP3.Invalidate();
            gbP4.Invalidate();

        }

        //This function is called when a player loses the game and it shows a message box to indicate which player has lost and disables the controls for that player
        public void loseEvent(string playerID)
        {
            switch (playerID)
            {
                //Check the player ID and show the corresponding message box and disable the controls for that player
                case "Player 1":
                    // If Player 1 has lost the game, show a message box to indicate that they have lost and disable the controls for Player 1
                    MessageBox.Show("Player 1 has lost the game!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbP1PlayableCard.Enabled = false;
                    btnP1Play.Enabled = false;
                    btnP1UNO.Enabled = false;
                    p1Lost = true;
                    break;
                case "Player 2":
                    // If Player 2 has lost the game, show a message box to indicate that they have lost and disable the controls for Player 2
                    MessageBox.Show("Player 2 has lost the game!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbP2PlayableCard.Enabled = false;
                    btnP2Play.Enabled = false;
                    btnP2UNO.Enabled = false;
                    p2Lost = true;
                    break;
                case "Player 3":
                    // If Player 3 has lost the game, show a message box to indicate that they have lost and disable the controls for Player 3
                    MessageBox.Show("Player 3 has lost the game!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbP3PlayableCard.Enabled = false;
                    btnP3Play.Enabled = false;
                    btnP3UNO.Enabled = false;
                    p3Lost = true;
                    break;
                case "Player 4":
                    // If Player 4 has lost the game, show a message box to indicate that they have lost and disable the controls for Player 4
                    MessageBox.Show("Player 4 has lost the game!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbP4PlayableCard.Enabled = false;
                    btnP4Play.Enabled = false;
                    btnP4UNO.Enabled = false;
                    p4Lost = true;
                    break;
            }
            // After a player loses, check if three players have lost the game and if not, call the nextTurnEvent function to continue the game with the next player
            if (!threePlayerExceedCardLimit())
            {
                nextTurnEvent(getPlayerName(playerID)); // Call the nextTurnEvent function to continue the game with the next player if three players have not lost the game
            }
        }


        public void winEvent(string playerID, int cardCount)
        {
            // Check if the player has no cards left and show a message box to indicate that they have won the game
            if (cardCount == 0)
            {
                MessageBox.Show(playerID + " has won the game!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Disable all the controls for all players to prevent further gameplay after a player has won the game
                btnP1Play.Enabled = false;
                btnP1UNO.Enabled = false;
                btnP2Play.Enabled = false;
                btnP2UNO.Enabled = false;
                btnP3Play.Enabled = false;
                btnP2UNO.Enabled = false;
                btnP4Play.Enabled = false;
                btnP4UNO.Enabled = false;
                btnPickup.Enabled = false;

                btnReset.Enabled = true; // Enable the reset button to allow players to start a new game after a player has won the game
            }
        }

        public bool threePlayerExceedCardLimit()
        {
            // Check if three players have lost the game and return true if they have, indicating that the remaining player has won the game, otherwise return false
            if (p1Lost && p2Lost && p3Lost)
            {
                cardCountP4 = 0; // Set the card count for Player 4 to 0 to indicate that they have won the game
                winEvent(gbP4.Text, cardCountP4);
                return true;
            }
            else if (p4Lost && p2Lost && p3Lost)
            {
                cardCountP1 = 0; // Set the card count for Player 1 to 0 to indicate that they have won the game
                winEvent(gbP1.Text, cardCountP1);
                return true;
            }
            else if (p4Lost && p1Lost && p3Lost)
            {
                cardCountP2 = 0; // Set the card count for Player 2 to 0 to indicate that they have won the game
                winEvent(gbP2.Text, cardCountP2);
                return true;
            }
            else if (p4Lost && p2Lost && p1Lost)
            {
                cardCountP3 = 0; // Set the card count for Player 3 to 0 to indicate that they have won the game
                winEvent(gbP3.Text, cardCountP3);
                return true;
            }
            return false;
        }

        public void errorHandler(string errorMessage)
        {
            //Show error when anything goes wrong and reset the game
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //reset the game
            for (int i = 0; i < 25; i++)
            {
                cardsP1[i] = "";
                cardsP2[i] = "";
                cardsP3[i] = "";
                cardsP4[i] = "";
            }
            initializeGame();

        }

        public string randomStartTurn()
        {
            // Initializing variables
            string startingPlayer = "";
            Random rand = new Random();
            int playerIndex = rand.Next(1, 4);

            // Randomly select a starting player and disable the other players' controls
            if (playerIndex == 1)
            {
                // If Player 1 is selected as the starting player, disable the controls for the other players
                startingPlayer = "Player 1's Turn";

                cbP2PlayableCard.Enabled = false;
                cbP3PlayableCard.Enabled = false;
                cbP4PlayableCard.Enabled = false;

                btnP2Play.Enabled = false;
                btnP3Play.Enabled = false;
                btnP4Play.Enabled = false;

                btnP2UNO.Enabled = false;
                btnP3UNO.Enabled = false;
                btnP4UNO.Enabled = false;

                // Change the background color of the player group boxes to indicate which player's turn it is next
                p1bgColor = Color.Red;
                p2bgColor = Color.Crimson;
                p3bgColor = Color.Crimson;
                p4bgColor = Color.Crimson;
            }
            else if (playerIndex == 2)
            {
                // If Player 2 is selected as the starting player, disable the controls for the other players
                startingPlayer = "Player 2's Turn";

                cbP1PlayableCard.Enabled = false;
                cbP3PlayableCard.Enabled = false;
                cbP4PlayableCard.Enabled = false;

                btnP1Play.Enabled = false;
                btnP3Play.Enabled = false;
                btnP4Play.Enabled = false;

                btnP1UNO.Enabled = false;
                btnP3UNO.Enabled = false;
                btnP4UNO.Enabled = false;

                // Change the background color of the player group boxes to indicate which player's turn it is next
                p1bgColor = Color.Crimson;
                p2bgColor = Color.Red;
                p3bgColor = Color.Crimson;
                p4bgColor = Color.Crimson;
            }
            else if (playerIndex == 3)
            {
                // If Player 3 is selected as the starting player, disable the controls for the other players
                startingPlayer = "Player 3's Turn";

                cbP1PlayableCard.Enabled = false;
                cbP2PlayableCard.Enabled = false;
                cbP4PlayableCard.Enabled = false;

                btnP1Play.Enabled = false;
                btnP2Play.Enabled = false;
                btnP4Play.Enabled = false;

                btnP1UNO.Enabled = false;
                btnP2UNO.Enabled = false;
                btnP4UNO.Enabled = false;

                // Change the background color of the player group boxes to indicate which player's turn it is next
                p1bgColor = Color.Crimson;
                p2bgColor = Color.Crimson;
                p3bgColor = Color.Red;
                p4bgColor = Color.Crimson;
            }
            else if (playerIndex == 4)
            {
                // If Player 4 is selected as the starting player, disable the controls for the other players
                startingPlayer = "Player 4's Turn";

                cbP1PlayableCard.Enabled = false;
                cbP2PlayableCard.Enabled = false;
                cbP3PlayableCard.Enabled = false;

                btnP1Play.Enabled = false;
                btnP2Play.Enabled = false;
                btnP3Play.Enabled = false;

                btnP1UNO.Enabled = false;
                btnP2UNO.Enabled = false;
                btnP3UNO.Enabled = false;

                // Change the background color of the player group boxes to indicate which player's turn it is next
                p1bgColor = Color.Crimson;
                p2bgColor = Color.Crimson;
                p3bgColor = Color.Crimson;
                p4bgColor = Color.Red;
            }

            // Return the starting player
            return startingPlayer;
        }


        // Do the below section after learning the mouse click event
        public void displayPlayerCards()
        {
            string flippedCard = "";
            if (cbP1PlayableCard.Enabled)
            {
                flippedCard = cbP1PlayableCard.Text;
                //Flip a random card to start the game and display it on the picture box
                switch (flippedCard)
                {
                    //Check the value of the flipped card and display the corresponding image in the picture box
                    case string c when c.Contains("Zero"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_0;
                                break;

                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_0;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_0;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_0;
                                break;

                        }
                        break;
                    case string c when c.Contains("One"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_1;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_1;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_1;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_1;
                                break;
                        }
                        break;
                    case string c when c.Contains("Pickup 2"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_pickup_2;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_pickup_2;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_pickup_2;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_pickup_2;
                                break;
                        }
                        break;
                    case string c when c.Contains("Three"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_3;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_3;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_3;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_3;
                                break;
                        }
                        break;
                    case string c when c.Contains("Four"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_4;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_4;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_4;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_4;
                                break;
                        }
                        break;
                    case string c when c.Contains("Five"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_5;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_5;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_5;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_5;
                                break;
                        }
                        break;
                    case string c when c.Contains("Six"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_6;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_6;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_6;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_6;
                                break;
                        }
                        break;
                    case string c when c.Contains("Seven"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_7;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_7;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_7;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_7;
                                break;
                        }
                        break;
                    case string c when c.Contains("Eight"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_8;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_8;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_8;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_8;
                                break;
                        }
                        break;
                    case string c when c.Contains("Nine"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_9;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_9;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_9;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_9;
                                break;
                        }
                        break;
                    case string c when c.Contains("Skip Next"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_skip_next;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_skip_next;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_skip_next;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_skip_next;
                                break;
                        }
                        break;
                    case string c when c.Contains("Reverse"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_reverse;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_reverse;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_reverse;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_reverse;
                                break;
                        }
                        break;
                    case string c when c.Contains("Two"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_2;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_2;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_2;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_2;
                                break;
                        }
                        break;
                    case string c when c.Contains("Pickup 4"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_pickup_4;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_pickup_4;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_pickup_4;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_pickup_4;
                                break;
                            case string card when card.Contains("Switch"):
                                pbCardsP1.Image = Properties.Resources.wild_pickup_4_reverse;
                                break;
                        }
                        break;
                    case string c when c.Contains("Discard"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_discard;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_discard;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_discard;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_discard;
                                break;
                        }
                        break;
                    case string c when c.Contains("Skip All"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP1.Image = Properties.Resources.red_skip_all;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP1.Image = Properties.Resources.green_skip_all;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP1.Image = Properties.Resources.blue_skip_all;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP1.Image = Properties.Resources.yellow_skip_all;
                                break;
                        }
                        break;
                    //Check for wild cards and display the corresponding image in the picture box
                    case string c when c.Contains("Pickup 6"):
                        pbCardsP1.Image = Properties.Resources.wild_pickup_6;
                        break;
                    case string c when c.Contains("Pickup 10"):
                        pbCardsP1.Image = Properties.Resources.wild_pickup_10;
                        break;
                    case string c when c.Contains("Roulette"):
                        pbCardsP1.Image = Properties.Resources.wild_roulette;
                        break;
                }
            }
            else if (cbP2PlayableCard.Enabled)
            {
                flippedCard = cbP2PlayableCard.Text;
                //Flip a random card to start the game and display it on the picture box
                switch (flippedCard)
                {
                    //Check the value of the flipped card and display the corresponding image in the picture box
                    case string c when c.Contains("Zero"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_0;
                                break;

                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_0;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_0;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_0;
                                break;

                        }
                        break;
                    case string c when c.Contains("One"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_1;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_1;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_1;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_1;
                                break;
                        }
                        break;
                    case string c when c.Contains("Pickup 2"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_pickup_2;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_pickup_2;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_pickup_2;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_pickup_2;
                                break;
                        }
                        break;
                    case string c when c.Contains("Three"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_3;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_3;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_3;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_3;
                                break;
                        }
                        break;
                    case string c when c.Contains("Four"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_4;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_4;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_4;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_4;
                                break;
                        }
                        break;
                    case string c when c.Contains("Five"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_5;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_5;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_5;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_5;
                                break;
                        }
                        break;
                    case string c when c.Contains("Six"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_6;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_6;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_6;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_6;
                                break;
                        }
                        break;
                    case string c when c.Contains("Seven"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_7;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_7;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_7;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_7;
                                break;
                        }
                        break;
                    case string c when c.Contains("Eight"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_8;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_8;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_8;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_8;
                                break;
                        }
                        break;
                    case string c when c.Contains("Nine"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_9;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_9;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_9;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_9;
                                break;
                        }
                        break;
                    case string c when c.Contains("Skip Next"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_skip_next;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_skip_next;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_skip_next;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_skip_next;
                                break;
                        }
                        break;
                    case string c when c.Contains("Reverse"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_reverse;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_reverse;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_reverse;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_reverse;
                                break;
                        }
                        break;
                    case string c when c.Contains("Two"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_2;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_2;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_2;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_2;
                                break;
                        }
                        break;
                    case string c when c.Contains("Pickup 4"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_pickup_4;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_pickup_4;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_pickup_4;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_pickup_4;
                                break;
                            case string card when card.Contains("Switch"):
                                pbCardsP2.Image = Properties.Resources.wild_pickup_4_reverse;
                                break;
                        }
                        break;
                    case string c when c.Contains("Discard"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_discard;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_discard;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_discard;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_discard;
                                break;
                        }
                        break;
                    case string c when c.Contains("Skip All"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP2.Image = Properties.Resources.red_skip_all;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP2.Image = Properties.Resources.green_skip_all;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP2.Image = Properties.Resources.blue_skip_all;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP2.Image = Properties.Resources.yellow_skip_all;
                                break;
                        }
                        break;
                    //Check for wild cards and display the corresponding image in the picture box
                    case string c when c.Contains("Pickup 6"):
                        pbCardsP2.Image = Properties.Resources.wild_pickup_6;
                        break;
                    case string c when c.Contains("Pickup 10"):
                        pbCardsP2.Image = Properties.Resources.wild_pickup_10;
                        break;
                    case string c when c.Contains("Roulette"):
                        pbCardsP2.Image = Properties.Resources.wild_roulette;
                        break;

                }
            }
            else if (cbP3PlayableCard.Enabled)
            {
                flippedCard = cbP3PlayableCard.Text;

                //Flip a random card to start the game and display it on the picture box
                switch (flippedCard)
                {
                    //Check the value of the flipped card and display the corresponding image in the picture box
                    case string c when c.Contains("Zero"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_0;
                                break;

                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_0;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_0;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_0;
                                break;

                        }
                        break;
                    case string c when c.Contains("One"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_1;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_1;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_1;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_1;
                                break;
                        }
                        break;
                    case string c when c.Contains("Pickup 2"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_pickup_2;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_pickup_2;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_pickup_2;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_pickup_2;
                                break;
                        }
                        break;
                    case string c when c.Contains("Three"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_3;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_3;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_3;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_3;
                                break;
                        }
                        break;
                    case string c when c.Contains("Four"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_4;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_4;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_4;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_4;
                                break;
                        }
                        break;
                    case string c when c.Contains("Five"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_5;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_5;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_5;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_5;
                                break;
                        }
                        break;
                    case string c when c.Contains("Six"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_6;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_6;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_6;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_6;
                                break;
                        }
                        break;
                    case string c when c.Contains("Seven"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_7;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_7;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_7;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_7;
                                break;
                        }
                        break;
                    case string c when c.Contains("Eight"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_8;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_8;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_8;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_8;
                                break;
                        }
                        break;
                    case string c when c.Contains("Nine"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_9;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_9;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_9;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_9;
                                break;
                        }
                        break;
                    case string c when c.Contains("Skip Next"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_skip_next;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_skip_next;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_skip_next;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_skip_next;
                                break;
                        }
                        break;
                    case string c when c.Contains("Reverse"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_reverse;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_reverse;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_reverse;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_reverse;
                                break;
                        }
                        break;
                    case string c when c.Contains("Two"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_2;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_2;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_2;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_2;
                                break;
                        }

                        break;
                    case string c when c.Contains("Pickup 4"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_pickup_4;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_pickup_4;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_pickup_4;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_pickup_4;
                                break;
                            case string card when card.Contains("Switch"):
                                pbCardsP3.Image = Properties.Resources.wild_pickup_4_reverse;
                                break;
                        }
                        break;
                    case string c when c.Contains("Discard"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_discard;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_discard;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_discard;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_discard;
                                break;
                        }
                        break;
                    case string c when c.Contains("Skip All"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP3.Image = Properties.Resources.red_skip_all;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP3.Image = Properties.Resources.green_skip_all;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP3.Image = Properties.Resources.blue_skip_all;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP3.Image = Properties.Resources.yellow_skip_all;
                                break;
                        }
                        break;
                    //Check for wild cards and display the corresponding image in the picture box
                    case string c when c.Contains("Pickup 6"):
                        pbCardsP3.Image = Properties.Resources.wild_pickup_6;
                        break;
                    case string c when c.Contains("Pickup 10"):
                        pbCardsP3.Image = Properties.Resources.wild_pickup_10;
                        break;
                    case string c when c.Contains("Roulette"):
                        pbCardsP3.Image = Properties.Resources.wild_roulette;
                        break;

                }
            }
            // If Player 4 is active, get the selected card from their combo box
            else if (cbP4PlayableCard.Enabled)
            {
                flippedCard = cbP4PlayableCard.Text;

                //Flip a random card to start the game and display it on the picture box
                switch (flippedCard)
                {
                    //Check the value of the flipped card and display the corresponding image in the picture box
                    case string c when c.Contains("Zero"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_0;
                                break;

                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_0;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_0;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_0;
                                break;

                        }
                        break;
                    case string c when c.Contains("One"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_1;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_1;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_1;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_1;
                                break;
                        }
                        break;
                    case string c when c.Contains("Pickup 2"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_pickup_2;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_pickup_2;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_pickup_2;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_pickup_2;
                                break;
                        }
                        break;
                    case string c when c.Contains("Three"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_3;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_3;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_3;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_3;
                                break;
                        }
                        break;
                    case string c when c.Contains("Four"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_4;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_4;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_4;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_4;
                                break;
                        }
                        break;
                    case string c when c.Contains("Five"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_5;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_5;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_5;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_5;
                                break;
                        }
                        break;
                    case string c when c.Contains("Six"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_6;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_6;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_6;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_6;
                                break;
                        }
                        break;
                    case string c when c.Contains("Seven"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_7;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_7;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_7;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_7;
                                break;
                        }
                        break;
                    case string c when c.Contains("Eight"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_8;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_8;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_8;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_8;
                                break;
                        }
                        break;
                    case string c when c.Contains("Nine"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_9;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_9;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_9;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_9;
                                break;
                        }
                        break;
                    case string c when c.Contains("Skip Next"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_skip_next;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_skip_next;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_skip_next;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_skip_next;
                                break;
                        }
                        break;
                    case string c when c.Contains("Reverse"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_reverse;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_reverse;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_reverse;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_reverse;
                                break;
                        }
                        break;
                    case string c when c.Contains("Two"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_2;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_2;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_2;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_2;
                                break;
                        }
                        break;
                    case string c when c.Contains("Pickup 4"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_pickup_4;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_pickup_4;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_pickup_4;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_pickup_4;
                                break;
                            case string card when card.Contains("Switch"):
                                pbCardsP4.Image = Properties.Resources.wild_pickup_4_reverse;
                                break;
                        }
                        break;
                    case string c when c.Contains("Discard"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_discard;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_discard;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_discard;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_discard;
                                break;
                        }
                        break;
                    case string c when c.Contains("Skip All"):
                        switch (flippedCard)
                        {
                            case string card when card.Contains("Red"):
                                pbCardsP4.Image = Properties.Resources.red_skip_all;
                                break;
                            case string card when card.Contains("Green"):
                                pbCardsP4.Image = Properties.Resources.green_skip_all;
                                break;
                            case string card when card.Contains("Blue"):
                                pbCardsP4.Image = Properties.Resources.blue_skip_all;
                                break;
                            case string card when card.Contains("Yellow"):
                                pbCardsP4.Image = Properties.Resources.yellow_skip_all;
                                break;
                        }
                        break;
                    //Check for wild cards and display the corresponding image in the picture box
                    case string c when c.Contains("Pickup 6"):
                        pbCardsP4.Image = Properties.Resources.wild_pickup_6;
                        break;
                    case string c when c.Contains("Pickup 10"):
                        pbCardsP4.Image = Properties.Resources.wild_pickup_10;
                        break;
                    case string c when c.Contains("Roulette"):
                        pbCardsP4.Image = Properties.Resources.wild_roulette;
                        break;

                }
            }
        }


        // CHANGE ALL THE ERROR HANDLERS TO THE NEW PLAYER LOSE METHOD YOU'LL MAKE LATER, YOU DON"T WANT GAME RESETTING EVERY TIME A PLAYER MAKES A MISTAKE, INSTEAD THEY SHOULD LOSE AND THE GAME SHOULD CONTINUE WITH THE OTHER PLAYERS UNTIL THERE'S A WINNER
        private void btnPickup_Click(object sender, EventArgs e)
        {
            try
            {
                string pickedCard = pickRandomCard();

                // Check which player's turn it is and add the picked card to their hand and update the corresponding combo box
                if (cbP1PlayableCard.Enabled)
                {
                    // If Player 1 is active, add the picked card to their hand and update the combo box
                    if (cardCountP1 < 24)
                    {
                        cardCountP1++;
                        cardsP1[cardCountP1] = pickedCard;
                        cbP1PlayableCard.Items.Add(pickedCard);
                    }
                    else
                    {
                        // If the player's hand is full, they are eliminated from the game
                        loseEvent(gbP1.Text);
                    }
                }
                else if (cbP2PlayableCard.Enabled)
                {
                    // If Player 2 is active, add the picked card to their hand and update the combo box
                    if (cardCountP2 < 24)
                    {
                        cardCountP2++;
                        cardsP2[cardCountP2] = pickedCard;
                        cbP2PlayableCard.Items.Add(pickedCard);
                    }
                    else
                    {
                        //  If the player's hand is full, they are eliminated from the game
                        loseEvent(gbP2.Text);
                    }

                }
                else if (cbP3PlayableCard.Enabled)
                {
                    // If Player 3 is active, add the picked card to their hand and update the combo box
                    if (cardCountP3 < 24)
                    {
                        cardCountP3++;
                        cardsP3[cardCountP3] = pickedCard;
                        cbP3PlayableCard.Items.Add(pickedCard);
                    }
                    else
                    {
                        // If the player's hand is full, they are eliminated from the game
                        loseEvent(gbP3.Text);
                    }
                }
                else if (cbP4PlayableCard.Enabled)
                {
                    // If Player 4 is active, add the picked card to their hand and update the combo box
                    if (cardCountP4 < 24)
                    {
                        cardCountP4++;
                        cardsP4[cardCountP4] = pickedCard;
                        cbP4PlayableCard.Items.Add(pickedCard);
                    }
                    else
                    {
                        // If the player's hand is full, they are eliminated from the game
                        loseEvent(gbP4.Text);
                    }
                }
                else
                {
                    // If no player's combo box is enabled, show an error message
                    errorHandler("No player is currently active. Please try again.");
                }
            }
            catch
            {
                // If anything goes wrong while picking up a card, show an error message and reset the game
                errorHandler("An error occurred while picking up a card. Please try again.");
            }
        }


        //btn play events must use initializeCards(), not pickRandomCard() 
        private void btnP1Play_Click(object sender, EventArgs e)
        {
            try
            {
                //Initialize a variable to hold the card that the player will play
                string pickedCard = pickRandomCard();

                // Check if the player has only one card left and hasn't called UNO, if so, add 2 cards to their hand as a penalty
                if (unoEvent(cardCountP1))
                {
                    // If Player 1 is active, add the picked card to their hand and update the combo box
                    for (int i = 0; i < 2; i++)
                    {
                        // If the player's hand is full, eliminate them from the game and show a message box with the player's name
                        if (cardCountP1 > 25)
                        {
                            loseEvent(gbP1.Text);
                            return;
                        }
                        else
                        {
                            // If the player's hand is not full, add the picked card to their hand and update the combo box
                            cardCountP1++;
                            cardsP1[cardCountP1] = pickedCard;
                            cbP1PlayableCard.Items.Add(pickedCard);
                        }
                    }
                    MessageBox.Show("Player 1 has played a card without calling UNO! 2+ cards penalty.");

                }

                else
                {
                    string cardToPlay = cbP1PlayableCard.Text;

                    //If three players have more than 25 cards in their hand, the game should end and show a message box with the player's name as the winner
                    if (threePlayerExceedCardLimit())
                    {
                        return;
                    }

                    if (cardToPlay == "")
                    {
                        MessageBox.Show("Please select a card to play.");
                        return;
                    }
                    else
                    {
                        if (cardHandler(cardToPlay, gbP2.Text, gbP1.Text))
                        {
                            // If the player has more than one card left, they can play a card and update the combo box and picture box
                            if (!cardToPlay.Contains("(Switch)"))
                            {
                                setDiscardDeck(cardToPlay);
                            }
                            // Remove the played card from the player's hand and update the combo box
                            for (int i = 0; i < cardCountP1; i++)
                            {
                                if (cardsP1[i] == cardToPlay && !string.IsNullOrEmpty(cardsP1[i]) && !(cardsP1[i].Contains("Seven") || cardsP1[i].Contains("Zero")))
                                {
                                    cardsP1[i] = cardsP1[i + 1];
                                    cardsP1[cardCountP1] = ""; // Clear the last card in the player's hand
                                    cardCountP1--; // Decrease the card count for Player 1
                                    break;
                                }
                            } // MAKE THE CARDS STACKABLE ACROSS COLOUR
                            cbP1PlayableCard.Items.Remove(cardToPlay); // Remove the played card from the combo box

                            while ((cbP1PlayableCard.Enabled == true && btnP1Play.Enabled == true) && (cbP2PlayableCard.Enabled == true && btnP2Play.Enabled == true) && (cbP3PlayableCard.Enabled == true && btnP3Play.Enabled == true) && (cbP4PlayableCard.Enabled == true && btnP4Play.Enabled == true))
                            {
                                // Check if Player 1 has won the game after playing the card, if so, show a message box with the player's name and reset the game
                                winEvent(gbP1.Text, cardCountP1);
                            }
                            if (cardCountP1 == 0)
                            {
                                pbCardsP1.Image = null; // Clear the picture box for Player 1 if they have no cards left
                            }
                            else
                            {
                                pbCardsP1.Image = Properties.Resources.any_card_rear; // Display the back of the card in the picture box for Player 1
                            }
                        }
                        else
                        {
                            MessageBox.Show("You cannot play that card. Please select a valid card to play.");
                        }
                    }

                }

            }
            catch
            {
                errorHandler("An error occurred while playing a card. Please try again.");
            }
        }

        private void btnP1UNO_Click(object sender, EventArgs e)
        {
            try
            {
                string pickedCard = pickRandomCard();


                // Check if the player has only one card left and has called UNO, if not, add 2 cards to their hand as a penalty
                if (!unoEvent(cardCountP1))
                {
                    // If Player 1 is active, add the picked card to their hand and update the combo box
                    for (int i = 0; i <= 2; i++)
                    {
                        // If the player's hand is full, eliminate them from the game and show a message box with the player's name
                        if (cardCountP1 > 25)
                        {
                            loseEvent(gbP1.Text);
                            return;
                        }
                        else
                        {
                            // If the player's hand is not full, add the picked card to their hand and update the combo box
                            cardCountP1++;
                            cardsP1[cardCountP1] = pickedCard;
                            cbP1PlayableCard.Items.Add(pickedCard);
                        }

                    }
                    MessageBox.Show("Player 1 has called UNO at the wrong time! 2+ cards penalty.");

                }
                else
                {
                    string cardToPlay = cbP1PlayableCard.Text;

                    //If three players have more than 25 cards in their hand, the game should end and show a message box with the player's name as the winner
                    if (threePlayerExceedCardLimit())
                    {
                        return;
                    }

                    if (cardToPlay == null)
                    {
                        MessageBox.Show("Please select a card to play.");
                        return;
                    }
                    else
                    {
                        if (cardHandler(cardToPlay, gbP2.Text, gbP1.Text))
                        {
                            // Call UNO successfully, so the player can play a card and update the combo box and picture box
                            if (!cardToPlay.Contains("(Switch)"))
                            {
                                setDiscardDeck(cardToPlay);
                            }
                            for (int i = 0; i < cardCountP1; i++) // Loop through the player's hand to find the card that was played and remove it from the hand
                            {
                                if (cardsP1[i] == cardToPlay && !string.IsNullOrEmpty(cardsP1[i]) && !(cardsP1[i].Contains("Seven") || cardsP1[i].Contains("Zero")))
                                {
                                    cardsP1[i] = cardsP1[i + 1];
                                    cardsP1[cardCountP1] = ""; // Clear the last card in the player's hand
                                    cardCountP1--; // Decrease the card count for Player 1
                                    break;
                                }
                            }
                            cbP1PlayableCard.Items.Remove(cardToPlay);

                            // Check if Player 1 has won the game after playing the card, if so, show a message box with the player's name and reset the game
                            winEvent(gbP1.Text, cardCountP1);
                            if (cardCountP1 == 0)
                            {
                                pbCardsP1.Image = null; // Clear the picture box for Player 1 if they have no cards left

                            }
                            else
                            {
                                pbCardsP1.Image = Properties.Resources.any_card_rear; // Display the back of the card in the picture box for Player 1
                            }
                        }
                        else
                        {
                            MessageBox.Show("You cannot play that card. Please select a valid card to play.");
                        }
                    }
                }
            }
            catch
            {
                errorHandler("An error occurred while calling UNO. Please try again.");
            }
        }

        private void btnP2Play_Click(object sender, EventArgs e)
        {
            try
            {
                string pickedCard = pickRandomCard();

                // Check if the player has only one card left and hasn't called UNO, if so, add 2 cards to their hand as a penalty
                if (unoEvent(cardCountP2))
                {
                    // If Player 2 is active, add the picked card to their hand and update the combo box
                    for (int i = 0; i < 2; i++)
                    {
                        // If the player's hand is full, eliminate them from the game and show a message box with the player's name
                        if (cardCountP2 > 25)
                        {
                            loseEvent(gbP2.Text);
                            return;
                        }
                        else
                        {
                            // If the player's hand is not full, add the picked card to their hand and update the combo box
                            cardCountP2++;
                            cardsP2[cardCountP2] = pickedCard;
                            cbP2PlayableCard.Items.Add(pickedCard);
                        }
                    }
                    MessageBox.Show("Player 2 has played a card without calling UNO! 2+ cards penalty.");

                }
                else
                {
                    string cardToPlay = cbP2PlayableCard.Text;

                    //If three players have more than 25 cards in their hand, the game should end and show a message box with the player's name as the winner
                    if (threePlayerExceedCardLimit())
                    {
                        return;
                    }

                    if (cardToPlay == "")
                    {
                        MessageBox.Show("Please select a card to play.");
                        return;
                    }
                    else
                    {
                        if (cardHandler(cardToPlay, gbP3.Text, gbP2.Text))
                        {
                            // If the player has more than one card left, they can play a card and update the combo box and picture box
                            if (!cardToPlay.Contains("(Switch)"))
                            {
                                setDiscardDeck(cardToPlay);
                            }
                            for (int i = 0; i < cardCountP2; i++) // Loop through the player's hand to find the card that was played and remove it from the hand
                            {
                                if (cardsP2[i] == cardToPlay && !string.IsNullOrEmpty(cardsP2[i]) && !(cardsP2[i].Contains("Seven") || cardsP2[i].Contains("Zero")))
                                {
                                    cardsP2[i] = cardsP2[i + 1];
                                    cardsP2[cardCountP2] = ""; // Clear the last card in the player's hand
                                    cardCountP2--; // Decrease the card count for Player 2
                                    break;
                                }
                            }
                            cbP2PlayableCard.Items.Remove(cardToPlay);

                            // Check if Player 2 has won the game after playing the card, if so, show a message box with the player's name and reset the game
                            winEvent(gbP2.Text, cardCountP2);
                            if (cardCountP2 == 0)
                            {
                                pbCardsP2.Image = null; // Clear the picture box for Player 2 if they have no cards left
                            }
                            else
                            {
                                pbCardsP2.Image = Properties.Resources.any_card_rear; // Display the back of the card in the picture box for Player 2
                            }
                        }
                        else
                        {
                            MessageBox.Show("You cannot play that card. Please select a valid card to play.");
                        }
                    }
                }
            }
            catch
            {
                errorHandler("An error occurred while playing a card. Please try again.");
            }
        }

        private void btnP2UNO_Click(object sender, EventArgs e)
        {
            try
            {
                string pickedCard = pickRandomCard();

                if (!unoEvent(cardCountP2))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (cardCountP2 > 25)
                        {
                            loseEvent(gbP2.Text);
                            return;
                        }
                        else
                        {
                            cardCountP2++;
                            cardsP2[cardCountP2] = pickedCard;
                            cbP2PlayableCard.Items.Add(pickedCard);
                        }
                    }
                    MessageBox.Show("Player 2 has called UNO at the wrong time! 2+ cards penalty.");
                }
                else
                {
                    string cardToPlay = cbP2PlayableCard.Text;

                    //If three players have more than 25 cards in their hand, the game should end and show a message box with the player's name as the winner
                    if (threePlayerExceedCardLimit())
                    {
                        return;
                    }

                    if (cardToPlay == "")
                    {
                        MessageBox.Show("Please select a card to play.");
                        return;
                    }
                    else
                    {
                        if (cardHandler(cardToPlay, gbP3.Text, gbP2.Text))
                        {
                            // Call UNO successfully, so the player can play a card and update the combo box and picture box
                            if (!cardToPlay.Contains("(Switch)"))
                            {
                                setDiscardDeck(cardToPlay);
                            }
                            for (int i = 0; i < cardCountP2; i++) // Loop through the player's hand to find the card that was played and remove it from the hand
                            {
                                if (cardsP2[i] == cardToPlay && !string.IsNullOrEmpty(cardsP2[i]) && !(cardsP2[i].Contains("Seven") || cardsP2[i].Contains("Zero")))
                                {
                                    cardsP2[i] = cardsP2[i + 1];
                                    cardsP2[cardCountP2] = ""; // Clear the last card in the player's hand
                                    cardCountP2--; // Decrease the card count for Player 2
                                    break;
                                }
                            }
                            cbP2PlayableCard.Items.Remove(cardToPlay);

                            // Check if Player 2 has won the game after playing the card, if so, show a message box with the player's name and reset the game
                            winEvent(gbP2.Text, cardCountP2);
                            if (cardCountP2 == 0)
                            {
                                pbCardsP2.Image = null; // Clear the picture box for Player 2 if they have no cards left
                            }
                            else
                            {
                                pbCardsP2.Image = Properties.Resources.any_card_rear; // Display the back of the card in the picture box for Player 2
                            }
                        }
                        else
                        {
                            MessageBox.Show("You cannot play that card. Please select a valid card to play.");
                        }
                    }
                }
            }
            catch
            {
                errorHandler("An error occurred while playing a card. Please try again.");
            }
        }

        private void btnP3Play_Click(object sender, EventArgs e)
        {
            try
            {
                string pickedCard = pickRandomCard();

                if (unoEvent(cardCountP3))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (cardCountP3 > 25)
                        {
                            loseEvent(gbP3.Text);
                            return;
                        }
                        else
                        {
                            cardCountP3++;
                            cardsP3[cardCountP3] = pickedCard;
                            cbP3PlayableCard.Items.Add(pickedCard);
                        }
                    }
                    MessageBox.Show("Player 3 has played a card without calling UNO! 2+ cards penalty.");

                }
                else
                {
                    string cardToPlay = cbP3PlayableCard.Text;

                    //If three players have more than 25 cards in their hand, the game should end and show a message box with the player's name as the winner
                    if (threePlayerExceedCardLimit())
                    {
                        return;
                    }

                    if (cardToPlay == "")
                    {
                        MessageBox.Show("Please select a card to play.");
                        return;
                    }
                    else
                    {
                        if (cardHandler(cardToPlay, gbP4.Text, gbP3.Text))
                        {
                            // If the player has more than one card left, they can play a card and update the combo box and picture box
                            if (!cardToPlay.Contains("(Switch)"))
                            {
                                setDiscardDeck(cardToPlay);
                            }
                            for (int i = 0; i < cardCountP3; i++) // Loop through the player's hand to find the card that was played and remove it from the hand
                            {
                                if (cardsP3[i] == cardToPlay && !string.IsNullOrEmpty(cardsP3[i]) && !(cardsP3[i].Contains("Seven") || cardsP3[i].Contains("Zero")))
                                {
                                    cardsP3[i] = cardsP3[i + 1];
                                    cardsP3[cardCountP3] = ""; // Clear the last card in the player's hand
                                    cardCountP3--; // Decrease the card count for Player 3
                                    break;
                                }
                            }
                            cbP3PlayableCard.Items.Remove(cardToPlay);

                            // Check if Player 3 has won the game after playing the card, if so, show a message box with the player's name and reset the game
                            winEvent(gbP3.Text, cardCountP3);
                            if (cardCountP3 == 0)
                            {
                                pbCardsP3.Image = null; // Clear the picture box for Player 3 if they have no cards left

                            }
                            else
                            {
                                pbCardsP3.Image = Properties.Resources.any_card_rear; // Display the back of the card in the picture box for Player 3
                            }
                        }
                        else
                        {
                            MessageBox.Show("You cannot play that card. Please select a valid card to play.");
                        }
                    }
                }
            }
            catch
            {
                errorHandler("An error occurred while playing a card. Please try again.");
            }
        }

        private void btnP3UNO_Click(object sender, EventArgs e)
        {
            try
            {
                string pickedCard = pickRandomCard();

                if (!unoEvent(cardCountP3))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (cardCountP3 > 25)
                        {
                            loseEvent(gbP3.Text);
                            return;
                        }
                        else
                        {
                            cardCountP3++;
                            cardsP3[cardCountP3] = pickedCard;
                            cbP3PlayableCard.Items.Add(pickedCard);
                        }
                    }
                    MessageBox.Show("Player 3 has called UNO at the wrong time! 2+ cards penalty.");

                }
                else
                {
                    string cardToPlay = cbP3PlayableCard.Text;

                    //If three players have more than 25 cards in their hand, the game should end and show a message box with the player's name as the winner
                    if (threePlayerExceedCardLimit())
                    {
                        return;
                    }

                    if (cardToPlay == "")
                    {
                        MessageBox.Show("Please select a card to play.");
                        return;
                    }
                    else
                    {
                        if (cardHandler(cardToPlay, gbP4.Text, gbP3.Text))
                        {
                            // Call UNO successfully, so the player can play a card and update the combo box and picture box
                            if (!cardToPlay.Contains("(Switch)"))
                            {
                                setDiscardDeck(cardToPlay);
                            }
                            for (int i = 0; i < cardCountP3; i++) // Loop through the player's hand to find the card that was played and remove it from the hand
                            {
                                if (cardsP3[i] == cardToPlay && !string.IsNullOrEmpty(cardsP3[i]) && !(cardsP3[i].Contains("Seven") || cardsP3[i].Contains("Zero")))
                                {
                                    cardsP3[i] = cardsP3[i + 1];
                                    cardsP3[cardCountP3] = ""; // Clear the last card in the player's hand
                                    cardCountP3--; // Decrease the card count for Player 3
                                    break;
                                }
                            }
                            cbP3PlayableCard.Items.Remove(cardToPlay);

                            // Check if Player 3 has won the game after playing the card, if so, show a message box with the player's name and reset the game
                            winEvent(gbP3.Text, cardCountP3);
                            if (cardCountP3 == 0)
                            {
                                pbCardsP3.Image = null; // Clear the picture box for Player 3 if they have no cards left
                            }
                            else
                            {
                                pbCardsP3.Image = Properties.Resources.any_card_rear; // Display the back of the card in the picture box for Player 3
                            }
                        }
                        else
                        {
                            MessageBox.Show("You cannot play that card. Please select a valid card to play.");
                        }
                    }
                }
            }
            catch
            {
                errorHandler("An error occurred while playing a card. Please try again.");
            }
        }

        private void btnP4Play_Click(object sender, EventArgs e)
        {
            try
            {
                string pickedCard = pickRandomCard();

                if (unoEvent(cardCountP4))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (cardCountP4 > 25)
                        {
                            loseEvent(gbP4.Text);
                            return;
                        }
                        else
                        {
                            cardCountP4++;
                            cardsP4[cardCountP4] = pickedCard;
                            cbP4PlayableCard.Items.Add(pickedCard);
                        }
                    }
                    MessageBox.Show("Player 4 has played a card without calling UNO! 2+ cards penalty.");

                }
                else
                {
                    string cardToPlay = cbP4PlayableCard.Text;

                    //If three players have more than 25 cards in their hand, the game should end and show a message box with the player's name as the winner
                    if (threePlayerExceedCardLimit())
                    {
                        return;
                    }

                    if (cardToPlay == "")
                    {
                        MessageBox.Show("Please select a card to play.");
                        return;
                    }
                    else
                    {
                        if (cardHandler(cardToPlay, gbP1.Text, gbP4.Text))
                        {
                            // If the player has more than one card left, they can play a card and update the combo box and picture box
                            if (!cardToPlay.Contains("(Switch)"))
                            {
                                setDiscardDeck(cardToPlay);
                            }
                            for (int i = 0; i < cardCountP4; i++) // Loop through the player's hand to find the card that was played and remove it from the hand
                            {
                                if (cardsP4[i] == cardToPlay && !string.IsNullOrEmpty(cardsP4[i]) && !(cardsP4[i].Contains("Seven") || cardsP4[i].Contains("Zero")))
                                {
                                    cardsP4[i] = cardsP4[i + 1];
                                    cardsP4[cardCountP4] = ""; // Clear the last card in the player's hand
                                    cardCountP4--; // Decrease the card count for Player 4
                                    break;
                                }
                            }
                            cbP4PlayableCard.Items.Remove(cardToPlay);

                            // Check if Player 4 has won the game after playing the card, if so, show a message box with the player's name and reset the game
                            winEvent(gbP4.Text, cardCountP4);
                            if (cardCountP4 == 0)
                            {
                                pbCardsP4.Image = null; // Clear the picture box for Player 4 if they have no cards left
                            }
                            else
                            {
                                pbCardsP4.Image = Properties.Resources.any_card_rear; // Display the back of the card in the picture box for Player 4
                            }
                        }
                        else
                        {
                            MessageBox.Show("You cannot play that card. Please select a valid card to play.");
                        }
                    }
                }
            }
            catch
            {
                errorHandler("An error occurred while playing a card. Please try again.");
            }
        }
        private void btnP4UNO_Click(object sender, EventArgs e)
        {
            try
            {
                string pickedCard = pickRandomCard();

                // Check if the player has only one card left and has called UNO, if not, add 2 cards to their hand as a penalty
                if (!unoEvent(cardCountP4))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        // If the player's hand is full, eliminate them from the game and show a message box with the player's name
                        if (cardCountP4 > 25)
                        {
                            loseEvent(gbP4.Text);
                            return;
                        }
                        // If the player's hand is not full, add the picked card to their hand and update the combo box
                        else
                        {
                            cardCountP4++;
                            cardsP4[cardCountP4] = pickedCard;
                            cbP4PlayableCard.Items.Add(pickedCard);
                        }
                    }
                    // If Player 4 has played a card without calling UNO, show a message box with the player's name and the penalty
                    MessageBox.Show("Player 4 has called UNO at the wrong time! 2+ cards penalty.");
                }
                else
                {
                    string cardToPlay = cbP4PlayableCard.Text;

                    //If three players have more than 25 cards in their hand, the game should end and show a message box with the player's name as the winner
                    if (threePlayerExceedCardLimit())
                    {
                        return;
                    }
                    // If the player has called UNO successfully but hasn't selected a card to play, show an error message and return
                    if (cardToPlay == "")
                    {
                        MessageBox.Show("Please select a card to play.");
                        return;
                    }
                    else
                    {
                        if (cardHandler(cardToPlay, gbP1.Text, gbP4.Text))
                        {
                            // Call UNO successfully, so the player can play a card and update the combo box and picture box
                            if (!cardToPlay.Contains("(Switch)"))
                            {
                                setDiscardDeck(cardToPlay);
                            }
                            for (int i = 0; i < cardCountP4; i++) // Loop through the player's hand to find the card that was played and remove it from the hand
                            {
                                if (cardsP4[i] == cardToPlay && !string.IsNullOrEmpty(cardsP4[i]) && !(cardsP4[i].Contains("Seven") || cardsP4[i].Contains("Zero")))
                                {
                                    cardsP4[i] = cardsP4[i + 1];
                                    cardsP4[cardCountP4] = ""; // Clear the last card in the player's hand
                                    cardCountP4--; // Decrease the card count for Player 4
                                    break;
                                }
                            }
                            cbP4PlayableCard.Items.Remove(cardToPlay);

                            // Check if Player 4 has won the game after playing the card, if so, show a message box with the player's name and reset the game
                            winEvent(gbP4.Text, cardCountP4);
                            if (cardCountP4 == 0)
                            {
                                pbCardsP4.Image = null; // Clear the picture box for Player 4 if they have no cards left
                            }
                            else
                            {
                                pbCardsP4.Image = Properties.Resources.any_card_rear; // Display the back of the card in the picture box for Player 4
                            }
                        }
                        else
                        {
                            MessageBox.Show("You cannot play that card. Please select a valid card to play.");
                        }
                    }
                }
            }
            catch
            {
                errorHandler("An error occurred while playing a card. Please try again.");
            }
        }

        private void cbP2PlayableCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbP2PlayableCard.Text == "")
                {
                    pbCardsP2.Image = Properties.Resources.any_card_rear; // Display the back of the card if no card is selected
                }
                else
                {
                    // If Player 2 is active, display the selected card in the picture box
                    displayPlayerCards();
                }

            }
            catch
            {
                errorHandler("An error occurred while displaying the selected card. Please try again.");

            }
        }

        private void cbP1PlayableCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbP1PlayableCard.Text == "")
                {
                    pbCardsP1.Image = Properties.Resources.any_card_rear; // Display the back of the card if no card is selected
                }
                else
                {
                    // If Player 1 is active, display the selected card in the picture box
                    displayPlayerCards();
                }
            }
            catch
            {
                errorHandler("An error occurred while displaying the selected card. Please try again.");
            }
        }

        private void cbP3PlayableCard_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (cbP3PlayableCard.Text == "")
                {
                    pbCardsP3.Image = Properties.Resources.any_card_rear; // Display the back of the card if no card is selected
                }
                else
                {
                    // If Player 3 is active, display the selected card in the picture box
                    displayPlayerCards();
                }

            }
            catch
            {
                errorHandler("An error occurred while displaying the selected card. Please try again.");
            }
        }

        private void cbP4PlayableCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbP4PlayableCard.Text == "")
                {
                    pbCardsP4.Image = Properties.Resources.any_card_rear; // Display the back of the card if no card is selected
                }
                else
                {
                    // If Player 4 is active, display the selected card in the picture box
                    displayPlayerCards();
                }
            }
            catch
            {
                errorHandler("An error occurred while displaying the selected card. Please try again.");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset the game by clearing all players' hands, resetting the card counts, and reinitializing the game
            initializeCards();
            initializeGame();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush bgBrush = new SolidBrush(Color.Crimson); // mildly dark red color for the background

            g.FillRectangle(bgBrush, this.ClientRectangle); // Fill the entire form with the background color
        }

        private void gbP1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush p1IdentityColorBrush = new SolidBrush(Color.Firebrick); // Brush for custom designs per player
            SolidBrush bgBrush = new SolidBrush(p1bgColor); // mildly dark red color for the background
            Pen aroundP1Pen = new Pen(Color.Black, 5); // Black for the border

            g.FillRectangle(bgBrush, gbP1.ClientRectangle); // Fill the entire group box with the background color

            // Add some custom designs per player
            g.FillRectangle(p1IdentityColorBrush, 71, 0, 91, 292);
            g.FillRectangle(p1IdentityColorBrush, 0, 95, 232, 105);

            g.DrawRectangle(aroundP1Pen, gbP1.ClientRectangle); // Add a black border to the groupbox
         }

        private void gbPileAndPlaceDown_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush bgBrush = new SolidBrush(Color.Crimson); // mildly dark red color for the background
            // Table backdrop for the decks
            SolidBrush aroundTheDecksBrush = new SolidBrush(Color.BurlyWood);
            Pen aroundTheDecksPen = new Pen(Color.Black, 5);

            // Draw the rectangles
            g.FillRectangle(bgBrush, gbPileAndPlaceDown.ClientRectangle);
            g.FillRectangle(aroundTheDecksBrush, 150, 45, 300, 225);
            g.DrawRectangle(aroundTheDecksPen, 150, 45, 300, 225);
        }

        private void gbP2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush p2IdentityColorBrush = new SolidBrush(Color.DodgerBlue); // Brush for custom designs per player
            SolidBrush bgBrush = new SolidBrush(p2bgColor); // mildly dark red color for the background
            Pen aroundP1Pen = new Pen(Color.Black, 5); // Black for the border

            g.FillRectangle(bgBrush, gbP2.ClientRectangle); // Fill the entire group box with the background color

            // Add some custom designs per player
            g.FillRectangle(p2IdentityColorBrush, 71, 0, 91, 292);
            g.FillRectangle(p2IdentityColorBrush, 0, 95, 232, 105);

            g.DrawRectangle(aroundP1Pen, gbP2.ClientRectangle); // Add a black border to the groupbox
        }

        private void gbP3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush p3IdentityColorBrush = new SolidBrush(Color.Goldenrod); // Brush for custom designs per player
            SolidBrush bgBrush = new SolidBrush(p3bgColor); // mildly dark red color for the background
            Pen aroundP1Pen = new Pen(Color.Black, 5); // Black for the border

            g.FillRectangle(bgBrush, gbP3.ClientRectangle); // Fill the entire group box with the background color

            // Add some custom designs per player
            g.FillRectangle(p3IdentityColorBrush, 71, 0, 91, 292);
            g.FillRectangle(p3IdentityColorBrush, 0, 95, 232, 105);

            g.DrawRectangle(aroundP1Pen, gbP3.ClientRectangle); // Add a black border to the groupbox
        }

        private void gbP4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush p4IdentityColorBrush = new SolidBrush(Color.MediumOrchid); // Brush for custom designs per player
            SolidBrush bgBrush = new SolidBrush(p4bgColor); // mildly dark red color for the background
            Pen aroundP1Pen = new Pen(Color.Black, 5); // Black for the border

            g.FillRectangle(bgBrush, gbP4.ClientRectangle); // Fill the entire group box with the background color

            // Add some custom designs per player
            g.FillRectangle(p4IdentityColorBrush, 71, 0, 91, 292);
            g.FillRectangle(p4IdentityColorBrush, 0, 95, 232, 105);

            g.DrawRectangle(aroundP1Pen, gbP4.ClientRectangle); // Add a black border to the groupbox
        }
    }
}
