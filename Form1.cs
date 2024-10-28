namespace test_tictactoe
{
    public partial class Form1 : Form
    {
        private Button[,] buttons = new Button[3, 3];
        private bool isPlayerXTurn = true; // True if it's Player X's turn, false for Player O
        private int turnCount = 0; // Count of turns taken
        private Label titleLabel;
        private Label statusLabel;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartGame(); // Start the game when the form loads
        }

        private void StartGame()
        {
            InitializeGame(); // Initialize the game setup
        }

        private void InitializeGame()
        {
            this.Text = "Tic Tac Toe";
            this.Size = new Size(400, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(34, 36, 49);

            titleLabel = new Label
            {
                Text = "Tic Tac Toe - Player X's Turn",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 20, 0, 10)
            };

            statusLabel = new Label
            {
                Text = "",
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = Color.WhiteSmoke,
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 0, 0, 20)
            };

            // Create a TableLayoutPanel to arrange buttons
            TableLayoutPanel tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 3,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            // Set the row and column styles to ensure equal sizing
            for (int i = 0; i < 3; i++)
            {
                tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            }

            // Initialize buttons and add them to the TableLayoutPanel
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = new Button
                    {
                        Dock = DockStyle.Fill,
                        Font = new Font("Arial", 24, FontStyle.Bold),
                        ForeColor = Color.FromArgb(0, 122, 204),
                        BackColor = Color.FromArgb(50, 50, 50),
                        Tag = new Point(i, j), // Store the position in the Tag
                        Margin = new Padding(2),
                        FlatStyle = FlatStyle.Flat
                    };
                    button.FlatAppearance.BorderSize = 0;
                    button.Click += Button_Click;
                    tableLayout.Controls.Add(button, j, i); // Add button to TableLayoutPanel
                    buttons[i, j] = button; // Update the buttons array
                }
            }

            this.Controls.Add(tableLayout); // Add TableLayoutPanel to the Form
            this.Controls.Add(titleLabel);  // Add title label to the Form
            this.Controls.Add(statusLabel); // Add status label to the Form
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton.Text != "") return; // Ignore if the button is already clicked

            clickedButton.Text = isPlayerXTurn ? "X" : "O";
            clickedButton.ForeColor = isPlayerXTurn ? Color.FromArgb(0, 122, 204) : Color.FromArgb(204, 0, 122);

            Point position = (Point)clickedButton.Tag; // Get the position from the button's Tag
            buttons[position.X, position.Y] = clickedButton; // Update the buttons array
            turnCount++;

            if (CheckForWinner())
            {
                statusLabel.Text = $"Player {(isPlayerXTurn ? "X" : "O")} wins!";
                MessageBox.Show($"Player {(isPlayerXTurn ? "X" : "O")} wins!", "Game Over");
                ResetGame();
            }
            else if (turnCount == 9)
            {
                statusLabel.Text = "It's a draw!";
                MessageBox.Show("It's a draw!", "Game Over");
                ResetGame();
            }
            else
            {
                isPlayerXTurn = !isPlayerXTurn; // Switch turns
                titleLabel.Text = $"Tic Tac Toe - Player {(isPlayerXTurn ? "X" : "O")}'s Turn";
            }
        }

        private bool CheckForWinner()
        {
            // Check rows, columns, and diagonals for a winner
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Text != "" && buttons[i, 0].Text == buttons[i, 1].Text && buttons[i, 1].Text == buttons[i, 2].Text)
                    return true; // Row check
                if (buttons[0, i].Text != "" && buttons[0, i].Text == buttons[1, i].Text && buttons[1, i].Text == buttons[2, i].Text)
                    return true; // Column check
            }

            // Diagonal checks
            if (buttons[0, 0].Text != "" && buttons[0, 0].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 2].Text)
                return true; // Left diagonal
            if (buttons[0, 2].Text != "" && buttons[0, 2].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 0].Text)
                return true; // Right diagonal

            return false; // No winner
        }

        private void ResetGame()
        {
            foreach (var button in buttons)
            {
                button.Text = "";
                button.BackColor = Color.FromArgb(50, 50, 50);
            }
            turnCount = 0;
            isPlayerXTurn = true; // Reset to Player X's turn
            titleLabel.Text = "Tic Tac Toe - Player X's Turn";
            statusLabel.Text = ""; // Clear the status label
        }
    }
}
