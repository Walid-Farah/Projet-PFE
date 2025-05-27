using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEditor.SearchService;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor;



public class DataBase : MonoBehaviour
{
    private static string connectonString= "URI=file:Users.s3db";
    public TMP_InputField email;
    public TMP_InputField nom;
    public TMP_InputField prenom;
    public TMP_InputField mdp;
    public TMP_Text erreur;
    private Regex expressionPourMail;
    private Regex expressionPourNom;

    string query;

    public static string nomPersonne="Test",prenomPersonne="Test",emailPersonne="Test";
    public static int idPersonne=5,niveauPersonne = 0, NbrPoints = 0, NbrEssaie=0;



    void Start()
    {
        expressionPourMail = new Regex(@"\w+@[A-z]+\.[a-z]+");
        expressionPourNom = new Regex(@"[A-z]{3,}\w*");


        //Debug.Log(expressionPourNom.IsMatch("bvb"));
        //Debug.Log(expressionPourMail.IsMatch("ds"));
        //connectonString = "URI=file:Users.s3db";

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Connexion()
    {
        if(email.text=="admin" && mdp.text == "admin")
        {
            bool ok=EditorUtility.DisplayDialog("Avertissement", "Voulez-vous Testez le jeu ?","Oui","Non");
            if (!ok)
            {
                SceneManager.LoadScene("Admin");
            }
            else
            {
                nomPersonne = "admin";
                prenomPersonne= "admin";
                emailPersonne= "admin";
                ManageScene.LoadSampleScene();
            }
        }
        else
            using (IDbConnection connection = new SqliteConnection(connectonString))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {

                    query = $"SELECT count(*) FROM Personne where email=\"{email.text}\"";
                    command.CommandText = query;

                    int rowCount = Convert.ToInt32(command.ExecuteScalar());

                    if (rowCount == 1)
                    {
                        query = $"SELECT * FROM Personne where email=\"{email.text}\"";
                        emailPersonne = email.text;
                        command.CommandText = query;
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            string motdepasse = reader.GetString(2);
                            if (mdp.text == motdepasse)
                            {
                                idPersonne = reader.GetInt16(0);
                                nomPersonne = reader.GetString(3);
                                prenomPersonne = reader.GetString(4);
                                niveauPersonne = reader.GetInt16(5);
                                ManageScene.LoadSampleScene();
                            }
                            else
                            {
                                erreur.SetText("Mot de passe faux");
                                Debug.Log("Mot de passe faux");
                            }
                            connection.Close();
                            reader.Close();
                        }
                    }
                    else
                    {
                        erreur.SetText("Compte Inexistant");
                        Debug.Log("Inexistant");
                    }

                }
            }
    }




    public void Inscription()
    {
        using (IDbConnection connection = new SqliteConnection(connectonString))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                try
                {
                    

                    if ((email.text.Equals("")) || (mdp.text).Equals("") || (nom.text).Equals("") || (prenom.text).Equals(""))
                    {
                        erreur.SetText("Veuillez Remplir Tous Les Champs");
                    }
                    else
                    {
                        if (!expressionPourMail.IsMatch(email.text))
                        {
                            erreur.SetText("Email Non Valide");
                        }
                        else
                        {
                            if (!(expressionPourNom.IsMatch(nom.text) && expressionPourNom.IsMatch(prenom.text)))
                            {
                                erreur.SetText("Le Nom et le Prenom Doivent Contenir au moins de 3 Lettres");
                            }
                            else { 
                                string sql = $"INSERT into Personne(email,mdp,nom,prenom) VALUES (\"{email.text}\",\"{mdp.text}\",\"{nom.text}\",\"{prenom.text}\")";
                                emailPersonne = email.text;
                                nomPersonne = nom.text;
                                prenomPersonne = prenom.text;
                                command.CommandText = sql;
                                command.ExecuteNonQuery();


                                command.CommandText = $"SELECT id FROM Personne WHERE email=\"{email.text}\"";
                                idPersonne = Int16.Parse(command.ExecuteScalar().ToString());


                                connection.Close();
                                ManageScene.LoadSampleScene();
                            }
                        }
                    }
                    

                }
                catch (SqliteException e)
                {

                    erreur.SetText("Compte Existant Reesayer");
                    Debug.Log("Compte Existant Reesayer");
                    Debug.LogException(e);
                }
                
            }
        }
    }

    public static void CreationLignePE(int idScene)
    {
        using(IDbConnection connection=new SqliteConnection(connectonString))
        {
            connection.Open();
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = $"INSERT OR IGNORE INTO PointEssaie(idPersonne,idPiece) VALUES ({idPersonne},{idScene})";
                command.ExecuteNonQuery();
            }
        }
    }

    public static void PointEssaie(int idScene,int nbrPoint)
    {
        using (IDbConnection connection = new SqliteConnection(connectonString))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE PointEssaie set NbrPoints={nbrPoint},NbrEssaie=NbrEssaie+1 WHERE idPersonne={idPersonne} and idPiece={idScene}";
                command.ExecuteNonQuery();
            }
        }
    }


    public static void AjouterPointQZ(int score)
    {
        using (IDbConnection connection = new SqliteConnection(connectonString))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE Personne set PointQuiz={score} WHERE id={idPersonne} ";
                command.ExecuteNonQuery();
            }
        }
    }


    public static int ScorePersonne()
    {
        int scorePersonne;
        using (IDbConnection connection = new SqliteConnection(connectonString))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT PointQuiz FROM Personne WHERE id={idPersonne} ";
                scorePersonne= Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return scorePersonne;
    }


    public static int NbrEssaieQuiz()
    {
        int nbrEssaieQuiz;
        using (IDbConnection connection = new SqliteConnection(connectonString))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT NbrEssaieQuiz FROM Personne WHERE id={idPersonne} ";
                nbrEssaieQuiz = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return nbrEssaieQuiz;
    }

    public static void AjoutNbrEssaieQuiz(int nbrEssaieQuiz)
    {
        using (IDbConnection connection = new SqliteConnection(connectonString))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE Personne set NbrEssaieQuiz={nbrEssaieQuiz} WHERE id={idPersonne} ";
                command.ExecuteNonQuery();
            }
        }
    }

    public static void SupprimerApprenant(string id)
    {
        using (IDbConnection connection = new SqliteConnection(connectonString))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = $"DELETE FROM Personne WHERE id={id}; DELETE FROM PointEssaie WHERE idPersonne={id}";
                command.ExecuteNonQuery();
            }
        }
    }

    public static string[] RecupererNomPrenomPersonne(string id)
    {
        string[] ret = new string[2];
        using (IDbConnection connection = new SqliteConnection(connectonString))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT nom,prenom FROM Personne WHERE id="+id;
                IDataReader reader=command.ExecuteReader();
                reader.Read();
                ret[0]=reader.GetString(0);
                ret[1]=reader.GetString(1);
            }
        }
        return ret;
    }

    public static void AjouterQuizOuPression(string id,int i)
    {
        using (IDbConnection connection = new SqliteConnection(connectonString))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = (i==1) ? $"UPDATE Personne SET FaireQuiz=1 WHERE id="+id+ "; UPDATE Personne SET Niveau=FaireQuiz+FairePression WHERE id=" + id : $"UPDATE Personne SET FairePression=1 WHERE id=" + id + "; UPDATE Personne SET Niveau=FaireQuiz+FairePression WHERE id=" + id;
                command.ExecuteNonQuery();
            }
        }
    }

    public static int GetBDDNiveau(string id)
    {
        int niveau;
        using (IDbConnection connection = new SqliteConnection(connectonString))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Niveau FROM Personne WHERE id="+id;
                niveau= Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return niveau;
    }
}
