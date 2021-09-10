using System;
using System.Collections.Generic;
using OperacionFuegoQuasar.Contracts;

namespace OperacionFuegoQuasar.Services
{
    public class MessageService
    {
        /// <summary>
        /// Obtiene mensaje a través de la información que envían los satélites
        /// </summary>
        /// <param name="messages">Información de los satélites</param>
        /// <returns>Mensaje</returns>
        public static string GetMessage(List<SatelliteData> satellites)
        {
            List<List<string>> listMessages = GetListMessages(satellites);

            int maxWords = GetMaxElements(listMessages);

            listMessages = CompleteList(listMessages, maxWords);

            string message = CreateMessage(listMessages, maxWords);

            return message;
        }

        /// <summary>
        /// Obtiene el listado de mensajes omitiendo el primer elemento si es vacio (""), no se tiene en cuenta ya que puede ser el defasaje.
        /// En caso que sea el mensaje de un solo satélite, lo tiene en cuenta al primer elemento.
        /// </summary>
        private static List<List<string>> GetListMessages(List<SatelliteData> messages)
        {
            List<List<string>> list = new List<List<string>>();

            foreach (SatelliteData satelliteData in messages)
            {
                List<string> newList = new List<string>();

                for (int i = 0; i < satelliteData.Message.Length; i++)
                {
                    if (i != 0 || (i == 0 && !String.IsNullOrEmpty(satelliteData.Message[i])) || messages.Count == 1)
                        newList.Add(satelliteData.Message[i]);
                }

                list.Add(newList);
            }

            return list;
        }

        /// <summary>
        /// Obtiene la máxima cantidad de palabras del mensaje
        /// </summary>
        /// <param name="listMessages">Lista de mensajes</param>
        /// <returns>Cantidad de palabras del mensaje</returns>
        private static int GetMaxElements(List<List<string>> listMessages)
        {
            int maxWords = 0;

            foreach (List<string> message in listMessages)
            {
                maxWords = message.Count > maxWords ? message.Count : maxWords;
            }

            return maxWords;
        }

        /// <summary>
        /// Completa el mensaje con espacios en blanco para los casos dónde se quito el primer elemento vacio ("") y el mismo no era defasaje sino primer palabra
        /// </summary>
        /// <param name="listMessages">Lista de mensajes</param>
        /// <param name="max">Cantidad de palabras del mensaje</param>
        /// <returns>Lista de mensajes</returns>
        private static List<List<string>> CompleteList(List<List<string>> listMessages, int maxWords)
        {
            List<List<string>> list = new List<List<string>>();

            foreach (List<string> message in listMessages)
            {
                if (message.Count != maxWords)
                {
                    List<string> newList = new List<string>();
                    newList.Add("");
                    foreach (string i in message)
                    {
                        newList.Add(i);
                    }

                    list.Add(newList);
                }
                else
                {
                    list.Add(message);
                }
            }

            return list;
        }

        /// <summary>
        /// Crear mensaje
        /// </summary>
        /// <param name="listMessages">Lista de mensajes</param>
        /// <param name="max">Cantidad de palabras del mensaje</param>
        /// <returns>Mensaje</returns>
        private static string CreateMessage(List<List<string>> listMessages, int maxWords)
        {
            string messageReturn = String.Empty;

            bool messageComplete = true;

            for (int i = 0; i < maxWords; i++)
            {
                bool status = false;
                foreach (List<string> message in listMessages)
                {
                    status = String.IsNullOrEmpty(message[i].ToString()) ? false : true;

                    if (status)
                    {
                        messageReturn = String.Format("{0} {1}", messageReturn, message[i].ToString());
                        break;
                    }
                }

                if (!status)
                {
                    messageComplete = false;
                    break;
                }
            }

            if (!messageComplete)
                messageReturn = "No se pudo leer el mensaje.";

            return messageReturn.Trim();
        }
    }
}
