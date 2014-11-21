using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace RodiDeath
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static int resX = 1024;
        static int resY = 768;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Enemigo[] enemigos = new Enemigo[5000];
        Interfaz interfazEstado = new Interfaz(5);
        Meco meco = new Meco(2, 2, resX, resY);
        Enemigo enemigo = new Enemigo(30, 30, resX, resY);
        Mapa mapa = new Mapa(0, 0);
        int i = 0;

        Texture2D herba, cuadricula;
        SoundEffect melodiaInicio;
        VentanaDebug v = new VentanaDebug();


        Random coorRandX = new Random(2442557);
        Random coorRandY = new Random(545736);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = resX;
            this.graphics.PreferredBackBufferHeight = resY;
            this.graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch = new SpriteBatch(GraphicsDevice); //Le decimos a la nave que textura tiene que cargar
            meco.TexturaArriba = Content.Load<Texture2D>("img/Meco/Arriba");
            meco.TexturaAbajo = Content.Load<Texture2D>("img/Meco/Abajo");
            meco.TexturaDerecha = Content.Load<Texture2D>("img/Meco/Derecha");
            meco.TexturaIzquierda = Content.Load<Texture2D>("img/Meco/Izquierda");

            for (int i = 0; i < enemigos.Length; i++)
            {
                enemigos[i] = new Enemigo(coorRandX.Next(0, 31), coorRandY.Next(0, 20), resX, resY);
                enemigos[i].TexturaArriba = Content.Load<Texture2D>("img/Enemigos/DarkShadow/Arriba");
                enemigos[i].TexturaAbajo = Content.Load<Texture2D>("img/Enemigos/DarkShadow/Abajo");
                enemigos[i].TexturaDerecha = Content.Load<Texture2D>("img/Enemigos/DarkShadow/Derecha");
                enemigos[i].TexturaIzquierda = Content.Load<Texture2D>("img/Enemigos/DarkShadow/Izquierda");
            }

            enemigo.TexturaArriba = Content.Load<Texture2D>("img/Enemigos/DarkShadow/Arriba");
            enemigo.TexturaAbajo = Content.Load<Texture2D>("img/Enemigos/DarkShadow/Abajo");
            enemigo.TexturaDerecha = Content.Load<Texture2D>("img/Enemigos/DarkShadow/Derecha");
            enemigo.TexturaIzquierda = Content.Load<Texture2D>("img/Enemigos/DarkShadow/Izquierda");

            
            interfazEstado.TexturaCorazon = Content.Load<Texture2D>("img/Interfaz/corazon");
            interfazEstado.TexturaInterfaz = Content.Load<Texture2D>("img/Interfaz/InterfazSC");

            herba = Content.Load<Texture2D>("img/Herba");
            cuadricula = Content.Load<Texture2D>("img/cuadricula1024");

            // MUSICA

            melodiaInicio = Content.Load<SoundEffect>("sonidos/Hyrule");
            melodiaInicio.Play();
            v.Show();


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            v.label1.Text = "PosXCoor:  " + meco.pos.X;
            v.label2.Text = "PosYCoor:  " + meco.pos.Y;
            v.label3.Text = "ConPasos:  " + meco.ContPasos.ToString();
            v.label4.Text = "SeMueve:  " + meco.SeMueve.ToString();
            v.label5.Text = "PosX:  " + meco.posX;
            v.label6.Text = "PosY:  " + meco.posY;


            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                i++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Add))
            {
                if (interfazEstado.NumCorazones < 10)
                    interfazEstado.NumCorazones++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Subtract))
            {
                if (interfazEstado.NumCorazones > 1)
                    interfazEstado.NumCorazones--;
            }

            for (int j = 0; j < i; j++)
            {
                enemigos[j].actualizaVariables(meco.PosX, meco.PosY);
                enemigos[j].actualiza(gameTime);
            }

            meco.actualiza(gameTime);
            enemigo.actualizaVariables(meco.PosX, meco.PosY);
            enemigo.actualiza(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(herba, new Vector2(0, 0), Color.White);

            for (int j = 0; j < i; j++)
            {
                enemigos[j].Draw(spriteBatch);
            }

            meco.Draw(spriteBatch);
            enemigo.Draw(spriteBatch);
            interfazEstado.Draw(spriteBatch);
            spriteBatch.Draw(cuadricula, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
