using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class PointCell : MonoBehaviour, ICell
{
    //UI
    // public Text nameLabel;
    // public Text genderLabel;
    // public Text idLabel;
    public Text x_label;
    public Text y_label;
    public Text z_label;
    public Text alpha_label;
    public Text beta_label;
    public Text gamma_label;
    //Model
    private Point _pointList;
    private int _cellIndex;

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(Point point,int cellIndex)
    {
        _cellIndex = cellIndex;
        _pointList = point;

        // convert point.x as text with remaining 2 digits after .
        x_label.text = point.x.ToString("F2");
        y_label.text = point.y.ToString("F2");
        z_label.text = point.z.ToString("F2");
        alpha_label.text = point.alpha.ToString("F2");
        beta_label.text = point.beta.ToString("F2");
        gamma_label.text = point.gamma.ToString("F2");
    }

    
    private void ButtonListener()
    {
        // Debug.Log("Index : " + _cellIndex +  ", Name : " + _contactInfo.Name  + ", Gender : " + _contactInfo.Gender);
        Debug.Log("Index : " + _cellIndex +  ", x : " + _pointList.x  + ", y : " + _pointList.y + ", z : " + _pointList.z + ", alpha : " + _pointList.alpha + ", beta : " + _pointList.beta + ", gamma : " + _pointList.gamma);
    }
}
