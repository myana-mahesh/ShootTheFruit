using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Gaze : MonoBehaviour {
   Dictionary<string, string> questions = new Dictionary<string, string> ();
   [SerializeField] List<Transform> FruitPos = new List<Transform> ();
   [SerializeField] private ParticleSystem gunFire;
   [SerializeField] private AudioSource gunAudio;
   [SerializeField] private GameObject[] Fruits;
   [SerializeField] private GameObject canvas;
   public Transform CameraPostion;
   float delay = 0.4f;
   Text textToChange;
   Text ErrorMessage;
   int i = 0;
   string q;

   void Start () {
      questions.Add ("Shoot at Apple", "Apple");
      questions.Add ("Shoot at Banana", "Banana");
      questions.Add ("Shoot at Kiwi", "Kiwi");
      questions.Add ("Shoot at StrawBerry", "StrawBerry");
      canvas.SetActive (false);
      textToChange = GameObject.Find ("Player/Canvas/Text").GetComponent<Text> ();
      ErrorMessage = GameObject.Find ("Player/Canvas/ErrorMessage").GetComponent<Text> ();
      InsFruits ();
      q = DisplayQuestion ();
      //Debug.Log (q);
   }

   void Update () {
      RaycastHit hit;
      if (Physics.Raycast (CameraPostion.position, CameraPostion.TransformDirection (Vector3.forward), out hit) && hit.collider.tag == "Fruits" && (hit.collider.gameObject.name == q)) {
         i += 1;
         gunAudio.Play ();
         gunFire.Play ();
         Destroy (hit.collider.gameObject, delay);
         ErrorMessage.text = " ";
         Debug.Log (q);
         hit.collider.tag = "D";
         if (i == Fruits.Length) {
            Show ();
         } else {
            q = DisplayQuestion ();
            Debug.Log (q);

         }
      }
      if (hit.collider.tag == "Fruits" && hit.collider.gameObject.name != q) {
         ErrorMessage.text = "Abe ande Lavde voh " + hit.collider.gameObject.name + " hai";
      } else {
         ErrorMessage.text = " ";
      }
   }

   public void Show () {
      canvas.SetActive (true);
   }

   public string DisplayQuestion () {
      if (questions.Count > 0) {
         int random = Random.Range (0, questions.Count);
         textToChange.text = questions.Keys.ElementAt (random);
         string x = questions[questions.Keys.ElementAt (random)];
         questions.Remove (questions.Keys.ElementAt (random));
         return x;
      }
      return null;
   }

   public void InsFruits () {
      int a;
      for (int j = 0; j < Fruits.Length; j++) {
         a = Random.Range (0, FruitPos.Count - 1);
         Fruits[j].transform.position = FruitPos[a].transform.position;
         FruitPos.RemoveAt (a);
      }
   }
}