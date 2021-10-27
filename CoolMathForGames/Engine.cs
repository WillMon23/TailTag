using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MathLibrary;
using Raylib_cs;
using System.Diagnostics;


namespace TailTag
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex = 0;
        private Scene[] _scenes = new Scene[0];
        private static Icon[,] _buffer;
        Stopwatch _stopwatch = new Stopwatch();

        int _tally = 0;

        private Player _player;

        private Enemy _enemyOne;

        private Enemy _enemyTwo;

        private Enemy _enemyThree;

        /// <summary>
        /// Called to begin the application 
        /// </summary>
        public void Run()
        {
            // Call start for the entire application 
            Start();
            float currentTme = 0;
            float lastTime = 0;
            float deltTime = 0;
            // Loop until the application is told to close
            while (!_applicationShouldClose && !Raylib.WindowShouldClose())
            {
                //Get how much time has passed since the application started 
                currentTme = _stopwatch.ElapsedMilliseconds / 1000.0f;

                //Set delta time to be the diffrence in time from the last time recorded to the current time
                deltTime = currentTme - lastTime;

                //Update Application
                Update(deltTime);
                //Draw The Update
                Draw();

                //Set the last recorded to be the current time
                lastTime = currentTme;

            }
            // Called end for the entire application
            End();
        }

        /// <summary>
        /// Called when application starts 
        /// </summary>
        private void Start()
        {

            //Creats a window  using raylib
            Raylib.InitWindow(800, 450, "Math For Games");

            Raylib.SetTargetFPS(0);

            _stopwatch.Start();

            //Initulises the characters 
            Scene scene = new Scene();

            _player = new Player('>', 30, 225, 500, Color.GREEN, scene, "Player");

            _enemyOne = new Enemy('<', 800, 100, 30, "Enemy", scene, _player, Color.BLUE);

            _enemyTwo = new Enemy('<', 800, 300, 30, "Enemy", scene, _player, Color.BLUE);

            _enemyThree = new Enemy('<', 800, 200, 30, "Enemy", scene, _player, Color.BLUE);

            CircleCollider playerCircleCollider = new CircleCollider(20, _player);
            _player.Collider = playerCircleCollider;

            UIText healthText = new UIText(30, 3, "Health", Color.BLUE, 200, 100, 10);
            UIText livesText = new UIText(50, 3, "Lives", Color.RED, 10, 10, 50);
            PlayerHud playerHud = new PlayerHud(_player, healthText, livesText);
            scene.AddActor(_player);

            scene.AddActor(_enemyOne);
            AABBCollider EnemyOnesBoxCollider = new AABBCollider(40, 40, _enemyOne);
            _enemyOne.Collider = EnemyOnesBoxCollider;

            scene.AddActor(_enemyTwo);
            AABBCollider enemyTwoBoxCollider = new AABBCollider(40, 40, _enemyTwo);
            _enemyTwo.Collider = enemyTwoBoxCollider;

            scene.AddActor(_enemyThree);
            AABBCollider enemyThreeBoxCollider = new AABBCollider(40, 40, _enemyThree);
            _enemyThree.Collider = enemyThreeBoxCollider;

            scene.AddUIElement(playerHud);


            _currentSceneIndex = AddScene(scene);
        }

        /// <summary>
        /// Called to draw to the scene 
        /// </summary>
        private void Draw()
        {
            Console.CursorVisible = false;
            //Clear the stuff that was on the screen in the last frame 
            _buffer = new Icon[Console.WindowWidth - 1 , Console.WindowHeight - 1];

            // Resets the cursor position to the top
            Console.SetCursorPosition(0, 0);
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            //Adds all actor icon to buffer
            _scenes[_currentSceneIndex].Draw();
            _scenes[_currentSceneIndex].DrawUI();

            Raylib.EndDrawing();
            

        }

        /// <summary>
        /// Updates the application and notifies the console of any changes 
        /// </summary>
        private void Update(float deltTime)
        {
            Random rng = new Random();

            _scenes[_currentSceneIndex].Update(deltTime);
            _scenes[_currentSceneIndex].UpdateUI(deltTime); 
            
            if (!_enemyThree.Alive)
                _scenes[_currentSceneIndex].RemoveActor(_enemyThree);
            

            
            while (Console.KeyAvailable)
                Console.ReadKey(true);

            Enemy addition = new Enemy('<', 600, 50, 20, "Enemy", _scenes[_currentSceneIndex], _player, Color.BLUE);

            if (_tally >= 2000)
            {
                _tally = 0;

                if (rng.Next(1, 5) == 4)
                    addition = new Enemy('<', 700, 200, 20, "Enemy", _scenes[_currentSceneIndex], _player, Color.BLUE);

                else if (rng.Next(1, 5) == 3)
                    addition = new Enemy('<', 600, 100, 20, "Enemy", _scenes[_currentSceneIndex], _player, Color.PURPLE);

                else if (rng.Next(1, 5) == 2)
                    addition = new Enemy('<', 500, 400, 20, "Enemy", _scenes[_currentSceneIndex], _player, Color.GREEN);

                _scenes[_currentSceneIndex].AddActor(addition);

                AABBCollider additionThreeBoxCollider = new AABBCollider(40, 40, addition);
                addition.Collider = additionThreeBoxCollider;

            }
            _tally++;
           
        }

        /// <summary>
        /// Called once the game has been set to game over 
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
            Raylib.CloseWindow();
        }

        /// <summary>
        /// Created to append new scnene to the current listing of scene           
        /// </summary>
        /// <param name="scene">Scene being added to the current list of scens</param>
        /// <returns>returns the new ammount of scenes</returns>
        public int AddScene(Scene scene)
        {
            // Creats a Temporary array 
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copys all the values from old array info to the temp array
            for (int i = 0; i < _scenes.Length; i++)
                tempArray[i] = _scenes[i];

            //Sets adds the new scene to the new size
            tempArray[_scenes.Length] = scene;

            // Set the old array to the new array
            _scenes = tempArray;

            // returns the new allocated size
            return _scenes.Length - 1;
        }

        /// <summary>
        /// Get the nexy key in the input stream
        /// </summary>
        /// <returns>The key thst waspressed </returns>
        public static ConsoleKey GetNextKey()
        {
            //if there is no key being pressed. . . 
            if (!Console.KeyAvailable)

                //. . . return
                return 0;

            //Return the current key being pressed 
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// when called will end the game
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }

}
