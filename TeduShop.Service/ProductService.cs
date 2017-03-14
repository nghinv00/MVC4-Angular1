﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using TeduShop.Data;
using TeduShop.Model;
using TeduShop.Common;


namespace TeduShop.Service
{
    public class ProductService
    {
        public ShopOnlineEntities Connect_Enttity = new ShopOnlineEntities();

        public IList<ProductModel> GetAll(string keyword)
        {
            IList<ProductModel> result = new List<ProductModel>();

            result = Connect_Enttity.Products.Select(x => new ProductModel
            {
                ID = x.ID,
                Name = x.Name,
                Alias = x.Alias,
                CategoryID = x.CategoryID,
                Image = x.Image,
                MoreImages=x.MoreImages,
                Price=x.Price,
                Promotion=x.Promotion,
                Warranty=x.Warranty,
                Description = x.Description,
                Content = x.Content,
                MetaKeyword = x.MetaKeyword,
                MetaDescription = x.MetaDescription,
                CreateDate = (DateTime)(x.CreateDate),
                CreateBy = x.CreateBy,
                UpdatedDate = (DateTime)(x.UpdatedDate),
                UpdatedBy = (x.UpdatedBy),
                Status = x.Status,
                HomeFlag = x.HomeFlag,
                HotFlag = x.HotFlag,
                ViewCount = x.ViewCount,
                Tags=x.Tags

            }).ToList();

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.Name.Contains(keyword)).ToList();
            }
            else
            {
                result = result.ToList();
            }

            return result.ToList();

        }
        public IList<ProductModel> GetAllPage(string keyword, int page, int pageSize)
        {
            IList<ProductModel> result = new List<ProductModel>();

            result = Connect_Enttity.Products.Select(x => new ProductModel
            {
                ID = x.ID,
                Name = x.Name,
                Alias = x.Alias,
                CategoryID = x.CategoryID,
                Image = x.Image,
                MoreImages = x.MoreImages,
                Price = x.Price,
                Promotion = x.Promotion,
                Warranty = x.Warranty,
                Description = x.Description,
                Content = x.Content,
                MetaKeyword = x.MetaKeyword,
                MetaDescription = x.MetaDescription,
                CreateDate = (DateTime)(x.CreateDate),
                CreateBy = x.CreateBy,
                UpdatedDate = (DateTime)(x.UpdatedDate),
                UpdatedBy = (x.UpdatedBy),
                Status = x.Status,
                HomeFlag = x.HomeFlag,
                HotFlag = x.HotFlag,
                ViewCount = x.ViewCount,
                Tags = x.Tags

            }).ToList();

            if (!string.IsNullOrEmpty(keyword) )
            {
                result = result.Where(x => x.Name.Contains(keyword)).OrderByDescending(x => x.CreateDate).Skip((page) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                result = result.OrderByDescending(x => x.CreateDate).Skip((page) * pageSize).Take(pageSize).ToList();
            }

            return result.ToList();
        }

        public IList<ProductModel> GetId(int id)
        {
            IList<ProductModel> result = new List<ProductModel>();

            result = Connect_Enttity.Products.Where(x => x.ID == id).Select(x => new ProductModel
            {
                ID = x.ID,
                Name = x.Name,
                Alias = x.Alias,
                CategoryID = x.CategoryID,
                Image = x.Image,
                MoreImages = x.MoreImages,
                Price = x.Price,
                Promotion = x.Promotion,
                Warranty = x.Warranty,
                Description = x.Description,
                Content = x.Content,
                MetaKeyword = x.MetaKeyword,
                MetaDescription = x.MetaDescription,
                CreateDate = (DateTime)(x.CreateDate),
                CreateBy = x.CreateBy,
                UpdatedDate = (DateTime)(x.UpdatedDate),
                UpdatedBy = (x.UpdatedBy),
                Status = x.Status,
                HomeFlag = x.HomeFlag,
                HotFlag = x.HotFlag,
                ViewCount = x.ViewCount,
                Tags = x.Tags

            }).ToList();

            result = result.ToList();

            return result.ToList();
        }

        public IEnumerable<ProductModel> ReadPage(string keyword,int page, int pageSize)
        {
           return GetAllPage(keyword, page, pageSize);
        }
        public IEnumerable<ProductModel> Read(string keyword)
        {
            return GetAll(keyword);
        }
        public IEnumerable<ProductModel> ReadID(int id)
        {
            return GetId(id);
        }
        public void Deleteone(int Id)
        {

            var data = (from c in Connect_Enttity.Products where c.ID == Id select c).FirstOrDefault();

            if (data != null)
            {
                Connect_Enttity.Products.Remove(data);
                Connect_Enttity.SaveChanges();
                Dispose();
            }

        }

        public void DeleteAll(int[] Id)
        {
            if (Id != null)
            {
                foreach (var i in Id)
                {
                    var data = Connect_Enttity.Products.FirstOrDefault(x => x.ID.Equals(i));
                    Connect_Enttity.Products.Remove(data);
                    Connect_Enttity.SaveChanges();
                }
                Dispose();

            }

        }

        public void Create(ProductModel model)
        {
            var data = Connect_Enttity.Products.FirstOrDefault(x => x.ID == model.ID);
            if (data == null)
            {
                var entity = new Product();
                entity.Name = model.Name;
                entity.Alias = model.Alias;
                entity.CategoryID = model.CategoryID;
                entity.Image = model.Image;

                entity.MoreImages = model.MoreImages;
                entity.Price = model.Price;
                entity.Promotion = model.Promotion;
                entity.Warranty = model.Warranty;

                entity.Description = model.Description;
                entity.Content = model.Content;
                entity.MetaKeyword = model.MetaKeyword;
                entity.MetaDescription = model.MetaDescription;
                entity.CreateDate = model.CreateDate;
                entity.CreateBy = model.CreateBy;
                entity.UpdatedDate = model.UpdatedDate;
                entity.UpdatedBy = model.UpdatedBy;
                entity.Status = model.Status;
                entity.HomeFlag = model.HomeFlag;
                entity.HotFlag = model.HotFlag;
                entity.ViewCount = model.ViewCount;
                entity.Tags = model.Tags;

                Connect_Enttity.Products.Add(entity);
                Connect_Enttity.SaveChanges();

                if(!string.IsNullOrEmpty(model.Tags))
                {
                    int IdProduct = Connect_Enttity.Products.OrderByDescending(x=>x.ID).Take(1).FirstOrDefault().ID;
                    string[] tags = model.Tags.Split(',');
                    for(var i = 0; i<tags.Length;i++)
                    {
                        var TagId = StringHelper.ToUnsignString(tags[i]);
                         if(Connect_Enttity.Tags.Count(x=>x.ID == TagId) == 0)
                         {
                             Tag tag = new Tag();
                             tag.ID = TagId; ;
                             tag.Name = tags[i];
                             tag.Type = CommonConstants.ProductTag;
                             tag.C_ID = IdProduct;

                             Connect_Enttity.Tags.Add(tag);
                             //Connect_Enttity.SaveChanges();
                         }

                        var entityProductTag = new ProductTag();
                        entityProductTag.productID = IdProduct;
                        entityProductTag.TagID = TagId;
                        Connect_Enttity.ProductTags.Add(entityProductTag);
                        Connect_Enttity.SaveChanges();
                    }
                }

               Dispose();
            }
        }

        public void Update(ProductModel model)
        {
            var data = Connect_Enttity.Products.FirstOrDefault(x => x.ID == model.ID);
            if (data != null)
            {
                data.Name = model.Name;
                data.Alias = model.Alias;
                data.CategoryID = model.CategoryID;
                data.Image = model.Image;

                data.MoreImages = model.MoreImages;
                data.Price = model.Price;
                data.Promotion = model.Promotion;
                data.Warranty = model.Warranty;

                data.Description = model.Description;
                data.Content = model.Content;
                data.MetaKeyword = model.MetaKeyword;
                data.MetaDescription = model.MetaDescription;
                data.CreateDate = model.CreateDate;
                data.CreateBy = model.CreateBy;
                data.UpdatedDate = model.UpdatedDate;
                data.UpdatedBy = model.UpdatedBy;
                data.Status = model.Status;
                data.HomeFlag = model.HomeFlag;
                data.HotFlag = model.HotFlag;
                data.ViewCount = model.ViewCount;
                data.Tags = model.Tags;

                Connect_Enttity.SaveChanges();

                if (!string.IsNullOrEmpty(model.Tags))
                {
                    int IdProduct = model.ID;

                    DeleteOneProductTag(IdProduct);
                    DeleteOneTag(IdProduct);

                    string[] tags = model.Tags.Split(',');
                    for (var i = 0; i < tags.Length; i++)
                    {
                        var TagId = StringHelper.ToUnsignString(tags[i]);
                        if (Connect_Enttity.Tags.Count(x => x.ID == TagId) == 0)
                        {
                            Tag tag = new Tag();
                            tag.ID = TagId; ;
                            tag.Name = tags[i];
                            tag.Type = CommonConstants.ProductTag;
                            tag.C_ID = IdProduct;

                            Connect_Enttity.Tags.Add(tag);
                            //Connect_Enttity.SaveChanges();
                        }

                        var entityProductTag = new ProductTag();

                        entityProductTag.productID = IdProduct;
                        entityProductTag.TagID = TagId;
                        Connect_Enttity.ProductTags.Add(entityProductTag);

                        Connect_Enttity.SaveChanges();
                    }
                }
                //Dispose();
            }
        }

        public void DeleteOneTag(int Id)
        {
            var data = (from c in Connect_Enttity.Tags where c.C_ID == Id && c.Type == CommonConstants.ProductTag select c).ToList();

            if (data.Count > 0)
            {
                foreach (var row in data)
                {
                    var delete = (from c in Connect_Enttity.Tags where c.C_ID == row.C_ID && c.Type == CommonConstants.ProductTag select c).FirstOrDefault();
                    if (delete != null)
                    {
                        Connect_Enttity.Tags.Remove(delete);
                        Connect_Enttity.SaveChanges();
                    }
                }
                
                
            }
        }
        public void DeleteOneProductTag(int Id)
        {

            var data = (from c in Connect_Enttity.ProductTags where c.productID == Id select c).ToList();

            if (data.Count > 0 )
            {
                foreach ( var row in data)
                {
                    var delete = (from c in Connect_Enttity.ProductTags where c.ID == row.ID select c).FirstOrDefault();
                    if(delete != null)
                    {
                        Connect_Enttity.ProductTags.Remove(delete);
                    }
                }
                Connect_Enttity.SaveChanges();
            }

        }

        public void Dispose()
        {
            Connect_Enttity.Dispose();
        }
    }
}
