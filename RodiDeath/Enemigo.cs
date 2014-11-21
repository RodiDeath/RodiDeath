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
    class Enemigo
    {
        private int ahora = 0;
        public Vector2 pos; //posicion del Meco
        public Texture2D texturaDerecha; //textura del Meco
        public Texture2D texturaArriba;
        public Texture2D texturaAbajo;
        public Texture2D texturaIzquierda;
        private int velocidadMeco = 2;
        public int posX = 0;
        public int posY = 0;
        public int mecoPosX = 0;
        public int mecoPosY = 0;
        private int contPasos = 0;
        private bool seMueve = false;
        private int resX;
        private int resY;
        private char ultimaDireccion = 'D';

        //Para controlar el cambio de frame
        float temporizador = 0f;
        float intervalo = 80f;
        //Número de frames que tendrá la animación
        int numFrames = 4;
        int frameActual = 0;

        //Tamaño del cada uno de los fotogramas (el rectángulo del ejemplo del tutorial)
        int spriteAncho = 50;
        int spriteAlto = 45;

        Rectangle rectanguloOrigen;
        Rectangle rectanguloDestino;

        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        public bool SeMueve
        {
            get { return seMueve; }
            set { seMueve = value; }
        }

        public int ContPasos
        {
            get { return contPasos; }
            set { contPasos = value; }
        }

        public Vector2 Posicion
        {
            get { return pos; }
            set { pos = value; }
        }
        public Texture2D TexturaDerecha
        {
            get { return texturaDerecha; }
            set { texturaDerecha = value; }
        }
        public Texture2D TexturaAbajo
        {
            get { return texturaAbajo; }
            set { texturaAbajo = value; }
        }
        public Texture2D TexturaArriba
        {
            get { return texturaArriba; }
            set { texturaArriba = value; }
        }
        public Texture2D TexturaIzquierda
        {
            get { return texturaIzquierda; }
            set { texturaIzquierda = value; }
        }

        public Enemigo(int x, int y, int resX, int resY)
        {
            this.rectanguloDestino = new Rectangle(0, 0, this.spriteAncho, this.spriteAlto);
            rectanguloOrigen = new Rectangle(this.frameActual * this.spriteAncho, 0, this.spriteAncho, this.spriteAlto);
            this.resX = resX;
            this.resY = resY;
            this.posX = x;
            this.posY = y;
            pos = new Vector2(x * 32, y * 32 - 15);
        }

        public void actualiza(GameTime gameTime)
        {
            ActualizaTemporizador(gameTime);


            if (!estaAlLado())
            {
                if ((dondeEstaMeco() == 'I') && !seMueve)
                {
                    seMueve = true;
                    ultimaDireccion = 'I';
                }
                else
                    if ((dondeEstaMeco() == 'D') && !seMueve)
                    {
                        seMueve = true;
                        ultimaDireccion = 'D';
                    }
                    else
                        if ((dondeEstaMeco() == 'A') && !seMueve)
                        {
                            seMueve = true;
                            ultimaDireccion = 'A';
                        }
                        else
                            if ((dondeEstaMeco() == 'B') && !seMueve)
                            {
                                seMueve = true;
                                ultimaDireccion = 'B';
                            }
                if (!seMueve)
                {
                    frameActual = 0;
                    rectanguloOrigen = new Rectangle(this.frameActual * this.spriteAncho, 0, this.spriteAncho, this.spriteAlto);
                }
                
            }
            MueveMeco();
        }

        public void Draw(SpriteBatch batch)
        {
            //Pintamos el Meco
            if (ultimaDireccion == 'I')
            {
                batch.Draw(this.TexturaIzquierda, this.pos - new Vector2(3, 0), this.rectanguloOrigen, Color.White);
            }
            if (ultimaDireccion == 'D')
            {
                batch.Draw(this.TexturaDerecha, this.pos - new Vector2(3, 0), this.rectanguloOrigen, Color.White);
            }
            if (ultimaDireccion == 'A')
            {
                batch.Draw(this.TexturaArriba, this.pos - new Vector2(3, 0), this.rectanguloOrigen, Color.White);
            }
            if (ultimaDireccion == 'B')
            {
                batch.Draw(this.TexturaAbajo, this.pos - new Vector2(3, 0), this.rectanguloOrigen, Color.White);
            }
        }

        public void actualizaVariables(int mecoX, int mecoY)
        {
            this.mecoPosX = mecoX;
            this.mecoPosY = mecoY;
        }

        public char dondeEstaMeco()
        {
            char dondeEsta = 'I';

            
            if (this.mecoPosX > this.posX)
            {
                dondeEsta = 'D';
            }
            else
                if (this.mecoPosX < this.posX)
                {
                    dondeEsta = 'I';
                }
                else 
                    if (this.mecoPosY > this.posY)
                    {
                        dondeEsta = 'B';
                    }
                    else
                        if (this.mecoPosY < this.posY)
                        {
                            dondeEsta = 'A';
                        }
      
            return dondeEsta;
        }


        public void ActualizaTemporizador(GameTime gameTime)
        {
            //Le sumamos al temporizador los milisegundos transcurruridos
            temporizador += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //Si es mayor al tiempo que le hemos definido en el intervalo
            if (temporizador > intervalo)
            {
                //Pasamos al siguiente frame
                frameActual++;

                //Controlamos que no nos pasemos de frames ;)
                if (frameActual > numFrames - 1)
                {
                    frameActual = 0;
                }
                //Se pone el temporizador a cero
                temporizador = 0f;
            }
        }

        public void MueveMeco()
        {
            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            //    intervalo = 75f;
            //    velocidadMeco = 2;
            //    contPasos++;
            //}
            //if (Keyboard.GetState().IsKeyUp(Keys.Space))
            //{
            //    velocidadMeco = 1;
            //    intervalo = 100f;
            //}

            if (contPasos < 31 && seMueve)
            {
                if (ultimaDireccion == 'I')
                {
                    if (posX > 0)
                    {
                        pos.X -= velocidadMeco;
                    }
                    if (contPasos >= 30 && posX > 0)
                    {
                        posX--;
                    }
                }
                if (ultimaDireccion == 'D')
                {
                    if (posX < 31)
                    {
                        pos.X += velocidadMeco;
                    }
                    if (contPasos >= 30 && posX < 31)
                    {
                        posX++;
                    }
                }
                if (ultimaDireccion == 'A')
                {
                    if (posY > 1)
                    {
                        pos.Y -= velocidadMeco;
                    }
                    if (contPasos >= 30 && posY > 1)
                    {
                        posY--;
                    }
                }
                if (ultimaDireccion == 'B')
                {
                    if (posY < 20)
                    {
                        pos.Y += velocidadMeco;
                    }
                    if (contPasos >= 30 && posY < 20)
                    {
                        posY++;
                    }
                }

                rectanguloOrigen = new Rectangle(this.frameActual * this.spriteAncho, 0, this.spriteAncho, this.spriteAlto);
                contPasos++;
                contPasos++;
            }
            else
            {
                seMueve = false;
                contPasos = 0;
            }
        }

        private bool estaAlLado()
        {
            bool esta = false;
            // DIagonal
           /* if (((this.posX == this.mecoPosX + 1) || (this.posX == this.mecoPosX - 1)) && ((this.posY == this.mecoPosY + 1) || (this.posY == this.mecoPosY - 1)))
            {
                esta = true;
            }
            else */if ((this.posX == this.mecoPosX + 1) && (this.posY == this.mecoPosY))
            {
                esta = true;
            }
            else if ((this.posX == this.mecoPosX - 1) && (this.posY == this.mecoPosY))
            {
                esta = true;
            }
            else if ((this.posY == this.mecoPosY + 1) && (this.posX == this.mecoPosX))
            {
                esta = true;
            }
            else if ((this.posY == this.mecoPosY - 1) && (this.posX == this.mecoPosX))
            {
                esta = true;
            }

            return esta;
        }
    }
}
