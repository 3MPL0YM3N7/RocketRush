using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    bool isThrusting = false;

    // RotationInput() variables
    [SerializeField] float xRotate = 0f;
    [SerializeField] float yRotate = 0f;
    [SerializeField] float zRotate = 1f;

    // ThrustInput() variables
    [SerializeField] float xForce = 0f;
    [SerializeField] float yForce = 1f;
    [SerializeField] float zForce = 0f;

    // SFX
    [SerializeField] AudioClip rocketThrusterSFX;
    [SerializeField] AudioClip sideThrustSFX;

    //Particles
    [SerializeField] ParticleSystem rocketJetParticles;
    [SerializeField] ParticleSystem sideParticlesRight;
    [SerializeField] ParticleSystem sideParticlesLeft;

    void Start()
    {

    }

    void Update()
    {
        ThrustInput();
        RotationInput();
    }

    void ThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isThrusting = true;
            // fly up
            GetComponent<Rigidbody>().AddRelativeForce(xForce * Time.deltaTime, yForce * Time.deltaTime, zForce * Time.deltaTime);

            // play audio once
            if (!GetComponent<AudioSource>().isPlaying) 
            {
                GetComponent<AudioSource>().PlayOneShot(rocketThrusterSFX);
            }

            // particles
            rocketJetParticles.Play();
        }
        else if (!Input.GetKey(KeyCode.Space) && isThrusting)
        {
            isThrusting = false;
            GetComponent<AudioSource>().Stop();
            rocketJetParticles.Stop();
        }
    }

    void RotationInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().freezeRotation = true;
            transform.Rotate(xRotate * Time.deltaTime, yRotate * Time.deltaTime, zRotate * Time.deltaTime);
            GetComponent<Rigidbody>().freezeRotation = false;

            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().PlayOneShot(sideThrustSFX);
            }

            sideParticlesRight.Play();
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().freezeRotation = true;
            transform.Rotate(-xRotate * Time.deltaTime, -yRotate * Time.deltaTime, -zRotate * Time.deltaTime);
            GetComponent<Rigidbody>().freezeRotation = false;

            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().PlayOneShot(sideThrustSFX);
            }

            sideParticlesLeft.Play();
        }
        
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A))
        {
            if (sideParticlesRight.isPlaying)
            {
                sideParticlesRight.Stop();
                GetComponent<AudioSource>().Stop();
            }
        }
        
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D))
        {
            if (sideParticlesLeft.isPlaying)
            {
                sideParticlesLeft.Stop();
                GetComponent<AudioSource>().Stop();
            }
        }
    }
}
