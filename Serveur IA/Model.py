from flask import Flask,jsonify,request
from Prediction import FairePrediction,FaireClassifiction
import pandas as pd


app=Flask(__name__)


@app.route("/get/<int:id>",methods=["GET"])
def get(id):
    return jsonify({"test":f"test {id}"})

@app.route("/postPredireScore",methods=["POST"])
def postPredireScore():
    data=request.get_json()
    joueur={
            "Joueur":data["Joueur"],
            "Âge":int(data["Âge"]),
            "Domaine_Précédent":data["Domaine_Précédent"],
            "Expérience_Années":int(data["Expérience_Années"]),
            "Sexe":data["Sexe"]
            }
    print(joueur)
    joueur = pd.DataFrame([joueur])

    return jsonify({"resultat":FairePrediction(joueur)[0]})


@app.route("/postClassification",methods=["POST"])
def postClassification():
    data=request.get_json()
    joueur={
            "Âge":int(data["Âge"]),
            "Expérience_Années":int(data["Expérience_Années"]),
            "Score/600":int(data["Score"])
            }
    print(joueur)
    joueur = pd.DataFrame([joueur])

    return jsonify({"Domaine_Précédent":FaireClassifiction(joueur)[0],
                    "genre":FaireClassifiction(joueur)[1]})


if __name__=='__main__':
    app.run(debug=True)

