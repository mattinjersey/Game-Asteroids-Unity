using UnityEngine;
using System.Collections;
using System;

namespace Asteroids {

	[RequireComponent( typeof( PlayerController ) )]
	[RequireComponent( typeof( WrapScreen ) )]
	[RequireComponent( typeof( FireWeapon ) )]
	[RequireComponent( typeof( CollisionWithAsteroid ) )]
	[RequireComponent( typeof( PlayerDeath ) )]
	[RequireComponent( typeof( PlayerShield ) )]
	public class Player : MonoBehaviour {

		public event Action EventDied;
		private PlayerController controller;
		private CollisionWithAsteroid collisionWithAsteroid;
		private PlayerDeath playerDeath;
		private PlayerShield shield;	
		[SerializeField]
		private GameObject gameHolder;
		[SerializeField]
		private int startingLives = 1;
		public int Lives
		{
			get;
			private set;
		}
		public int Points
		{
			get;
			private set;
		}
		//===================================================
		// UNITY METHODS
		//===================================================

		/// <summary>
		/// Awake.
		/// </summary>
		void Start() {
			ResetGame();		
		}
		public void ResetGame()
		{
			Points = 0;
			Lives = startingLives;
		//	xReset();
		//	UpdateLives(Lives);
		//	UpdatePoints(Points);
			shield = GetComponent<PlayerShield>();
			gameObject.transform.position = Vector3.zero;
	//		showGameScreen();
			//gameObject.SetActive( true );
			shield.Show();
		}
		//===================================================
		// PUBLIC METHODS
		//===================================================

		

		//===================================================
		// PRIVATE METHODS
		//===================================================


		//===================================================
		// EVENTS METHODS
		//===================================================

		/// <summary>
		/// Called when [collision with asteroid].
		/// </summary>
		private void OnCollisionWithAsteroid( Asteroid asteroid ) {
			// destroy the asteroid
			asteroid.Collision( int.MaxValue );

			// if the shields are not on DIE!
			if( !shield.IsInvincible ) {
				controller.Reset();
				playerDeath.Die();
				//gameObject.SetActive( false );
			}
		}

		/// <summary>
		/// Called when [death complete]. If the player still has lives then respawn.
		/// </summary>
		private void OnDeathComplete() {
			if( EventDied != null ) {
				EventDied();
			}
		}

	}
}