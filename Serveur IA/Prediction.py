import joblib
import numpy as np
import pandas as pd

def FairePrediction(X):
    model=joblib.load("ProjetUnity/Gradient Boosting")
    scalar=joblib.load("ProjetUnity/scalar.joblib")
    ohe=joblib.load("ProjetUnity/oneHotEncoding.joblib")
    if "Joueur" in X:
        X=X.drop(columns="Joueur")
    X.loc[X["Domaine_Précédent"]!="Frigorifie",["Domaine_Précédent"]]="Non Frigorifie"
    Dos=ohe.transform(X[["Domaine_Précédent","Sexe"]]).toarray()
    caracteristique=ohe.categories_
    caracteristique=np.array(caracteristique).ravel()
    X_encoder=pd.DataFrame(Dos,columns=caracteristique)
    X_encoder.index=X.index
    X=pd.concat([X,X_encoder],axis=1).drop(columns=["Domaine_Précédent","Sexe"])
    X[["Expérience_Années_standardisé", "Âge_standardisé"]] = scalar.fit_transform(X[["Expérience_Années", "Âge"]])
    X=X.drop(columns=["Âge","Expérience_Années"])
    return model.predict(X)

def FaireClassifiction(X):
    model=joblib.load("ProjetUnity/MultiOutputClassifier.joblib")
    sc=joblib.load("ProjetUnity/sc.joblib")
    X=sc.transform(X)
    return model.predict(X)[0]


testJoueur={"Joueur":"test",
            "Âge":35,
            "Domaine_Précédent":"Mécanique Auto",
            "Expérience_Années":10,
            "Sexe":"Homme"}

print(FairePrediction(pd.DataFrame([testJoueur])))