using RaffleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for InventoryController
/// </summary>
public class InventoryController
{
    #region [DECLRATION]
    RaffleEntities _entities = new RaffleEntities();
    InventoryDataContext _invContext = new InventoryDataContext();
    #endregion
	public InventoryController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<Inventory> GetInvestoryList()
    {
        return _entities.Inventories.OrderBy(x => x.Category_ID).ToList();
    }
    public List<Category> GetCategoryList()
    {
        return _entities.Categories.OrderBy(x => x.Category_ID).ToList();
    }

    public List<Inventory> GetInventoryList()
    {
        return _entities.Inventories.OrderBy(x => x.Inventory_ID).ToList();
    }
    public List<BottleSize> GetBottlesizelist()
    {
        return _entities.BottleSizes.OrderBy(x => x.BottleSizeId).ToList();
    }
    public List<Heading> Getheadinglist()
    {
        return _entities.Headings.OrderBy(x => x.HeadingId).ToList();
    }
    public List<SubHeading> GetSubHeadingList()
    {
        return _entities.SubHeadings.OrderBy(x => x.SubHeadingId).ToList();
    }

    public List<GetWineSearchListResult> GetSearchList(string wineName, string barCode)
    {
        return _invContext.GetWineSearchList(wineName, barCode).ToList();
    }


    public GetrecSearchListResult GetRecSearchList(int RecNo, string barCode)
    {
        return _invContext.GetrecSearchList(RecNo, barCode).FirstOrDefault();

    }
    
    public int AddInventory(Inventory objInv,Inventory_Stock ObjInvStock)
    {
        try
        {
            _entities.Inventories.AddObject(objInv);
            _entities.SaveChanges();
            ObjInvStock.Inventory_ID = objInv.Inventory_ID;
            _entities.Inventory_Stock.AddObject(ObjInvStock);
            _entities.SaveChanges(); 
            return objInv.Inventory_ID;
            
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public int AddCategory(Category objCategory)
    {
        try
        {
            Category obj= _entities.Categories.Where(x => x.Category_Name.ToLower().Trim() == objCategory.Category_Name.ToLower().Trim()).FirstOrDefault();
            if (obj == null)
            {
                _entities.Categories.AddObject(objCategory);
                _entities.SaveChanges();
                return objCategory.Category_ID;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public int AddInventoryImport(Inventory objInventory)
    {
        try
        {
            Inventory objInv = _entities.Inventories.Where(x => x.RecNo == objInventory.RecNo || x.Wine_name.ToLower().Trim() == objInventory.Wine_name.ToLower().Trim()).FirstOrDefault();
            if (objInv == null)
            {
                _entities.Inventories.AddObject(objInventory);
                _entities.SaveChanges();
                Inventory_Stock ObjInvStock = new Inventory_Stock();
                ObjInvStock.Inventory_ID = objInv.Inventory_ID;
                ObjInvStock.Location1_name = objInv.Loc1;
                ObjInvStock.Location2_name = objInv.Loc2;
                ObjInvStock.Location3_name = objInv.Loc3;
                if (!string.IsNullOrEmpty(objInv.Count1))
                {
                    ObjInvStock.Location1_Qty = Convert.ToInt32(objInv.Count1);
                }
                if (!string.IsNullOrEmpty(objInv.Count2))
                {
                    ObjInvStock.Location2_Qty = Convert.ToInt32(objInv.Count2);
                }
                if (!string.IsNullOrEmpty(objInv.Count3))
                {
                    ObjInvStock.Location3_qty = Convert.ToInt32(objInv.Count2);
                }
                _entities.Inventory_Stock.AddObject(ObjInvStock);
                _entities.SaveChanges();

                return objInv.Inventory_ID;
            }
            else
            {

                objInv.BottlePrize = objInventory.BottlePrize;
                objInv.BottleSize_ID = objInventory.BottleSize_ID;
                objInv.Category_ID = objInventory.Category_ID;
                objInv.SubHeading_ID = objInventory.SubHeading_ID;
                objInv.Heading_ID = objInventory.Heading_ID;
                objInv.Barcode = objInventory.Barcode;
                objInv.Description = objInventory.Description;
                objInv.Cost_Wholesale = objInventory.Cost_Wholesale;
                objInv.Wine_name = objInventory.Wine_name;
                objInv.WinelistCategory = objInventory.WinelistCategory;
                objInv.WineListLineNo = objInventory.WineListLineNo;
                objInv.ModifiedDate = DateTime.Now;

                Inventory_Stock ObjInvStock = _entities.Inventory_Stock.Where(x => x.Inventory_ID == objInv.Inventory_ID).FirstOrDefault();
                if (ObjInvStock != null)
                {
                    ObjInvStock.Location1_name = objInventory.Loc1;
                    ObjInvStock.Location2_name = objInventory.Loc2;
                    ObjInvStock.Location3_name = objInventory.Loc3;

                    if (objInventory.Count1 != null)
                    {
                        ObjInvStock.Location1_Qty = Convert.ToInt32(objInventory.Count1);
                    }
                    if (ObjInvStock.Location2_Qty != null)
                    {
                        ObjInvStock.Location2_Qty = Convert.ToInt32(objInventory.Count2);
                    }
                    if (ObjInvStock.Location3_qty != null)
                    {
                        ObjInvStock.Location3_qty = Convert.ToInt32(objInventory.Count3);
                    }
                }
                else
                {
                    ObjInvStock = new Inventory_Stock();
                    ObjInvStock.Inventory_ID = objInv.Inventory_ID;
                    ObjInvStock.Location1_name = objInventory.Loc1;
                    ObjInvStock.Location2_name = objInventory.Loc2;
                    ObjInvStock.Location3_name = objInventory.Loc3;
                    if (objInventory.Count1 != null)
                    {
                        ObjInvStock.Location1_Qty = Convert.ToInt32(objInventory.Count1);
                    }
                    if (ObjInvStock.Location2_Qty != null)
                    {
                        ObjInvStock.Location2_Qty = Convert.ToInt32(objInventory.Count2);
                    }
                    if (ObjInvStock.Location3_qty != null)
                    {
                        ObjInvStock.Location3_qty = Convert.ToInt32(objInventory.Count3);
                    }
                    _entities.Inventory_Stock.AddObject(ObjInvStock);
                }
                _entities.SaveChanges();
                return 1;
            }

            return 0;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public int AddBottleSize(BottleSize objBottle)
    {
        try
        {
            BottleSize obj = _entities.BottleSizes.Where(x => x.BottleSizeName.ToLower().Trim() == objBottle.BottleSizeName.ToLower().Trim()).FirstOrDefault();
            if (obj == null)
            {
                _entities.BottleSizes.AddObject(objBottle);
                _entities.SaveChanges();
                return objBottle.BottleSizeId;
            }
            else
            {
                return 0;
            }

        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public int AddHeading(Heading objHeading)
    {
        try
        {
            Heading obj = _entities.Headings.Where(x => x.HeadingName.ToLower().Trim() == objHeading.HeadingName.ToLower().Trim()).FirstOrDefault();
            if (obj == null)
            {
                _entities.Headings.AddObject(objHeading);
                _entities.SaveChanges();
                return objHeading.HeadingId;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public int AddSubHeading(SubHeading objSubHeading)
    {
        try
        {
            SubHeading obj = _entities.SubHeadings.Where(x => x.SubHeadingName.ToLower().Trim() == objSubHeading.SubHeadingName.ToLower().Trim()).FirstOrDefault();
            if (obj == null)
            {
                _entities.SubHeadings.AddObject(objSubHeading);
                _entities.SaveChanges();
                return objSubHeading.SubHeadingId;
            }
            else
            {
                return 0;
            }
        }

        catch (Exception ex)
        {
            return -1;
        }
    }

    public int AddInventoryStock(Inventory_Stock objInvStock)
    {
        try
        {
            _entities.Inventory_Stock.AddObject(objInvStock);
            _entities.SaveChanges();
            return objInvStock.Inventory_stockID;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    

    public bool UpdateInventory(Inventory objInv,Inventory_Stock objInvStock)
    {
        try
        {
            Inventory _objInv = _entities.Inventories.Where(x => x.Inventory_ID == objInv.Inventory_ID).FirstOrDefault();
            
            if (_objInv != null)
            {
                _objInv.Wine_name = objInv.Wine_name;
                _objInv.Barcode = objInv.Barcode;
                _objInv.RecNo = objInv.RecNo;
                _objInv.Category_ID = objInv.Category_ID;
                _objInv.Description = objInv.Description;
                _objInv.BottleSize_ID = objInv.BottleSize_ID;
                _objInv.BottlePrize = objInv.BottlePrize;
                _objInv.Cost_Wholesale = objInv.Cost_Wholesale;
                _objInv.Heading_ID = objInv.Heading_ID;
                _objInv.SubHeading_ID = objInv.SubHeading_ID;
                _objInv.ModifiedDate = DateTime.Now;

                Inventory_Stock _objinvStock = _entities.Inventory_Stock.Where(x => x.Inventory_ID == objInvStock.Inventory_ID).FirstOrDefault();
                if (_objinvStock != null)
                {
                    _objinvStock.Location1_Qty = objInvStock.Location1_Qty;
                    _objinvStock.Location2_Qty = objInvStock.Location2_Qty;
                    _objinvStock.Location3_qty = objInvStock.Location3_qty;
                    _objinvStock.Location1_name = objInvStock.Location1_name;
                    _objinvStock.Location2_name = objInvStock.Location2_name;
                    _objinvStock.Location3_name = objInvStock.Location3_name;
                }
                _entities.SaveChanges();


                return true;
            }
            else
            {
                return false;

            }
        }
        catch (Exception ex)
        {

        }
        return true;
    }

    public bool UpdateManageStock(Inventory_Stock objInvStock)
    {
        try
        {
           
                Inventory_Stock _objinvStock = _entities.Inventory_Stock.Where(x => x.Inventory_ID == objInvStock.Inventory_ID).FirstOrDefault();
                if (_objinvStock != null)
                {
                    _objinvStock.Location1_Qty = objInvStock.Location1_Qty;
                    _objinvStock.Location2_Qty = objInvStock.Location2_Qty;
                    _objinvStock.Location3_qty = objInvStock.Location3_qty;
                    _objinvStock.Location1_name = objInvStock.Location1_name;
                    _objinvStock.Location2_name = objInvStock.Location2_name;
                    _objinvStock.Location3_name = objInvStock.Location3_name;
                    _entities.SaveChanges();
                return true;
                }
              
            
            else
            {
                return false;

            }
    }
        catch (Exception ex)
        {

        }
        return true;
    }

    public Inventory GetInventoryId(int Id)
    {
        Inventory objInv = _entities.Inventories.Where(x => x.Inventory_ID == Id).FirstOrDefault();
        return objInv;
    }
    public Inventory_Stock GetInventoryStockId(int id)
    {
        Inventory_Stock objInvStock = _entities.Inventory_Stock.Where(x => x.Inventory_ID == id).FirstOrDefault();
        return objInvStock;
    }

    public GetInventoryByIdResult GetInventoryWithStock(int inventoryID)
    {
        return _invContext.GetInventoryById(inventoryID).FirstOrDefault();
    }


    public Inventory_Stock GetInventoryStock(int StockId)
    {
        Inventory_Stock objInvsto = _entities.Inventory_Stock.Where(x => x.Inventory_stockID == StockId).FirstOrDefault();
        return objInvsto;
    }
    public int DeleteInventory(int id,int stockId)
    {

        Inventory objInventory = _entities.Inventories.Where(x => x.Inventory_ID == id).FirstOrDefault();
        if (objInventory != null)
        {
            _entities.Inventories.DeleteObject(objInventory);
            Inventory_Stock objInventoryStock = _entities.Inventory_Stock.Where(x => x.Inventory_ID == stockId).FirstOrDefault();
            if (objInventoryStock != null)
            {
                _entities.Inventory_Stock.DeleteObject(objInventoryStock);
            }
            _entities.SaveChanges();
           
        }
        return 1;
    }

  
   
    }