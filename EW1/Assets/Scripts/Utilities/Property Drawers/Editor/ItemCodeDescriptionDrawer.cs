
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))] // Determine which custom attribute this drawer relates to (ItemCodeDescriptionAttribute.cs)
public class ItemCodeDescriptionDrawer : PropertyDrawer // => Inherit from UnityClass 
{
    // Here we override the base property height to include the Item Code Field in the Unity Inspector (*2)
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Change the returned property height to be double to cater for the additional item code description that we will draw
        return EditorGUI.GetPropertyHeight(property) * 2;
    }

    // OnGoing Method that actually draw the custom property 
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that prefab override logic works on the entire property.

        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.Integer) // Check if the property type is a SerializeField of type Integer
        {

            EditorGUI.BeginChangeCheck(); // Start of check for changed values (if itemCode is changed on the Item UI, update the description displayed)

            // Draw item code at the half of the height determined (Re implement base item code description in UI)
            var newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height / 2), label, property.intValue);

            // Draw item description (Then implement the new custom field)
            EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2), "Item Description", GetItemDescription(property.intValue));



            // If item code value has changed, then set value to new value
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = newValue;
            }


        }


        EditorGUI.EndProperty();
    }

    // Retrieve the Item Description to display in the UI 
    private string GetItemDescription(int itemCode)
    {
        // Ref the item list and pass the current Item Code to retrieve corresponding description 
        SO_ItemList so_itemList;
        // Retrieve SO Item List from DB to ref in code
        so_itemList = AssetDatabase.LoadAssetAtPath("Assets/Scriptable Object Assets/Item/so_ItemList.asset",typeof(SO_ItemList)) as SO_ItemList;
        // Store all items details from SO list retrieved from db  
        List<ItemDetails> itemDetailsList = so_itemList.itemDetails;
        // Retrieve corresponding item detail to display from itemCode
        ItemDetails itemDetail = itemDetailsList.Find(x => x.itemCode == itemCode);

        if (itemDetail != null)
        {
            return itemDetail.itemDescription;
        }
        else
        {
            return "";
        }
    }
}


