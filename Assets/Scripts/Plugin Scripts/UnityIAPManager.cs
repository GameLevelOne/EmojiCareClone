using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class UnityIAPManager : MonoBehaviour,IStoreListener {
	public Text coinText;

	IStoreController storeController;
	IAppleExtensions appStoreController;
	IExtensionProvider storeExtensionProvider;

	bool isGooglePlayStoreSelected;
	bool purchaseInProgress;
	int selectedItemIndex = -1; // -1 -> no product
	int totalUserCoin = 0;
	string lastTransactionID;

	static string idCoin1 = "test.coin10";
	static string idCoin2 = "test.coin50";
	static string idCoin3 = "test.coin100";
	static int amountCoin1 = 10;
	static int amountCoin2 = 50;
	static int amountCoin3 = 100;

	// Use this for initialization
	void Start ()
	{
		if (storeController == null) {
			InitIAP ();	
		}
	}

	public void InitIAP ()
	{
		if (IsInitialized ()) {
			return;
		}
		var module = StandardPurchasingModule.Instance();
		var builder = ConfigurationBuilder.Instance(module);
		//isGooglePlayStoreSelected = Application.platform == RuntimePlatform.Android && module.androidStore == AndroidStore.GooglePlay;
		builder.Configure<IGooglePlayConfiguration>().SetPublicKey("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAm9AlRg1V8edQeUw2VNBkUP8bGBuRxouWNChR0RpWWHafSnJDBmLRuW3iR1ULsGZg/0a47kxBKSTpk1byOCK/KlzJov6tZXy5dsS2xAqBe4anFsrhE5s2RGxeqbJdTy42QhT3KNeVa1ScM/hUtKNEPGHAsSvZmq6CS1B8AjDf6QK1CNs9zXs/GD/S7GwE4Hl4jbZMAmhOMgdDxdfCC3qJtlKB0NsNmB9c3dhgPUkW4gNoZxEM7f6sT0XTi6eFbyJUH3GprfSL8Oue1gfkn+Jm8PhElgauGsJDQpjDjh87lF3UmSo1Iu9yxmTQkFBms2DGFyJCFZZfUSW6/TMH55R4OQIDAQAB");
		builder.AddProduct(idCoin1,ProductType.Consumable);
		builder.AddProduct(idCoin2,ProductType.Consumable);
		builder.AddProduct(idCoin3,ProductType.Consumable);

		UnityPurchasing.Initialize(this,builder);

	}

	bool IsInitialized(){
		// Only say we are initialized if both the Purchasing references are set.
		return storeController != null /*&& storeExtensionProvider != null*/;
	}

	public void OnInitialized (IStoreController controller, IExtensionProvider extensions)
	{
		storeController = controller;
		appStoreController = extensions.GetExtension<IAppleExtensions> ();
		appStoreController.RegisterPurchaseDeferredListener (OnDeferred);

		Debug.Log ("Available items:");

		foreach (var item in controller.products.all) {
			if (item.availableToPurchase) {
				Debug.Log (string.Join (" - ",
					new[] {
						item.metadata.localizedTitle,
						item.metadata.localizedDescription,
						item.metadata.isoCurrencyCode,
						item.metadata.localizedPrice.ToString (),
						item.metadata.localizedPriceString,
						item.transactionID,
						item.receipt
					}));
			}
		}

		if (controller.products.all.Length > 0) {
			selectedItemIndex = 0;
		}
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("Billing failed to initialize!");
		switch (error)
		{
			case InitializationFailureReason.AppNotKnown:
				Debug.LogError("Is your App correctly uploaded on the relevant publisher console?");
				break;
			case InitializationFailureReason.PurchasingUnavailable:
				// Ask the user if billing is disabled in device settings.
				Debug.Log("Billing disabled!");
				break;
			case InitializationFailureReason.NoProductsAvailable:
				// Developer configuration error; check product metadata.
				Debug.Log("No products available for purchase!");
				break;
		}
	}

	public void BuyProduct (string productId)
	{
		if (IsInitialized ()) {
			Product product = storeController.products.WithID (productId);

			if (product != null && product.availableToPurchase) {
				Debug.Log (string.Format ("Purchasing product asynchronously: '{0}'", product.definition.id));
				storeController.InitiatePurchase (product);
			} else {
				Debug.Log (string.Format ("Purchase product ['{0}'] failed. Product is not found or not available for purchase", product.definition.id));
			}
		} else {
			Debug.Log("Error. UnityIAP is not initialized");
		}
	}

	public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs e)
	{
		Debug.Log ("Purchase OK: " + e.purchasedProduct.definition.id);

		lastTransactionID = e.purchasedProduct.transactionID;
		purchaseInProgress = false;

		if (string.Equals (e.purchasedProduct.definition.id, idCoin1, System.StringComparison.Ordinal)) {
			UpdateCoin(amountCoin1);
		} else if (string.Equals (e.purchasedProduct.definition.id, idCoin2, System.StringComparison.Ordinal)) {
			UpdateCoin(amountCoin2);
		} else if (string.Equals (e.purchasedProduct.definition.id, idCoin3, System.StringComparison.Ordinal)) {
			UpdateCoin(amountCoin3);
		}

		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product item, PurchaseFailureReason r)
	{
		Debug.Log("Purchase failed: " + item.definition.id);
		Debug.Log(r);

		purchaseInProgress = false;
	}

	void OnDeferred(Product item)
	{
		Debug.Log("Purchase deferred: " + item.definition.id);
	}

	void UpdateCoin(int coin){
		totalUserCoin +=coin;
		coinText.text = totalUserCoin.ToString();
	}

}
