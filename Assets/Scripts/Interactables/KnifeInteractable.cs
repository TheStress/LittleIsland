using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeInteractable : Interactable
{
    [SerializeField] KnifeGame knifeGame;
    [SerializeField] Transform playAreaCenter;
    [SerializeField] Transform startPosition;
    [SerializeField] float playAreaBounds;
    [SerializeField] float failFloor;
    [SerializeField] float verticalForce;
    [SerializeField] float horizontalForce;


    Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        // Fail if the knife falls a certain amount
        if(transform.position.y <= failFloor) {
            Fail();
        }
    }

    private void PopUp() {
        // Calculating random direction
        Vector3 horizontalDir = new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y).normalized;

        // If the knife is outside of play area 
        if(!InPlayArea()) {
            Vector3 dirToCenter = (playAreaCenter.position - transform.position).normalized;
            horizontalDir = new Vector3(dirToCenter.x, 0, dirToCenter.z);
        }

        // Calculating force
        Vector3 appliedHorizontalForce = horizontalDir * horizontalForce;
        Vector3 appliedVerticalForce = Vector3.up * verticalForce;

        // Applying force
        rb.velocity = appliedVerticalForce + appliedHorizontalForce;

        // Incrementing counter for game
        knifeGame.CountIncrement();
    }

    private bool InPlayArea() {
        // Returning true if the player is within horizontal area
        Vector3 difference = transform.position - playAreaCenter.position;
        if (new Vector3(difference.x, 0, difference.z).magnitude < playAreaBounds) {
            return true;
        }
        return false;
    }
    public override void ClickedOn(RaycastHit hit) {
        if(!knifeGame.IsStarted()) {
            knifeGame.StartGame();
            PopUp();
        }
        else {
            PopUp();
        }
    }

    private void Fail() {
        knifeGame.EndGame(); // Ending game
        transform.position = startPosition.position; // Resetting knife position
        rb.velocity = Vector3.zero; // Resetting the speed
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(playAreaCenter.position, playAreaBounds*2); // showing play area
        Gizmos.DrawWireCube(playAreaCenter.position + new Vector3(0,failFloor-playAreaCenter.position.y,0), new Vector3(5, 0, 5)); // Showing fail floor
    }
}
