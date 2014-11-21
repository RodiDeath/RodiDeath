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
    class Interfaz
    {
        int numCorazones;
        Texture2D texturaCorazon;
        Texture2D texturaInterfaz;
        private Vector2 posCorazon;
        private Vector2 posInterfaz;

        public Interfaz()
        {
        }

        public Interfaz(int numCorazones)
        {
            this.numCorazones = numCorazones;
            this.posCorazon.Y = (768 - 32 + 3);
            this.posInterfaz.X = 0;
            this.posInterfaz.Y = 0;
        }

        public Vector2 PosicionInterfaz
        {
            get { return posInterfaz; }
            set { posInterfaz = value; }
        }

        public Vector2 PosicionCorazon
        {
            get { return posCorazon; }
            set { posCorazon = value; }
        }

        public Texture2D TexturaCorazon
        {
            get { return texturaCorazon; }
            set { texturaCorazon = value; }
        }

        public Texture2D TexturaInterfaz
        {
            get { return texturaInterfaz; }
            set { texturaInterfaz = value; }
        }

        public int NumCorazones
        {
            get { return numCorazones; }
            set { numCorazones = value; }
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(this.texturaInterfaz, this.posInterfaz, Color.White);

            for (int i = 0; i < this.numCorazones; i++)
            {
                this.posCorazon.X = ((32*3)+(i*32) + 4);
                batch.Draw(this.TexturaCorazon, this.posCorazon, Color.White);
            }
        }
    }
}
