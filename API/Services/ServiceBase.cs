using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.ViewModels;
using Core.Models;
using Core;

using API.Messages;
using log4net;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace API.Services
{

    public class ServiceBase<Tmodel, Tview> where Tview : ViewModelBase where Tmodel : ModelBase
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(ServiceBase<Tmodel, Tview>));
        protected readonly IUnitOfWork _unitOfWork;

        //static readonly object thisLock = new object();
        //static readonly object thisLock2 = new object();
        //static readonly object thisLock3 = new object();

        //static readonly object lockInserOrUpdate01 = new object();
        //static readonly object lockInserOrUpdate02 = new object();
        //static readonly object lockInserOrUpdate03 = new object();
        //static readonly object lockInserOrUpdate04 = new object();
        //static readonly object lockInserOrUpdate05 = new object();
        //static readonly object lockInserOrUpdate06 = new object();

        //static readonly object lockFindAllBy = new object();
        //static readonly object lockFindAllBy01 = new object();
        //static readonly object lockFindAllBy02 = new object();
        //static readonly object lockFindAllBy03 = new object();
        //static readonly object lockFindAllBy04 = new object();
        //static readonly object lockFindAllBy05 = new object();


        public static MapperConfiguration configViewToModel = new MapperConfiguration(cfg => { cfg.CreateMap<Tview, Tmodel>(); });
        public static IMapper mapperViewToModel = configViewToModel.CreateMapper();

        public static MapperConfiguration configModelToView = new MapperConfiguration(cfg => { cfg.CreateMap<Tmodel, Tview>(); });
        public static IMapper mapperModelToView = configModelToView.CreateMapper();


        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        #region sync

        internal int Count(Expression<Func<Tmodel, bool>> predicate)
        {
            return _unitOfWork.GetRepo<Tmodel>().Count(predicate);
        }

        public ResponseBase<Tview> Delete(int id, string userName, string password)
        {

            ResponseBase<Tview> response = (ResponseBase<Tview>)Activator.CreateInstance(typeof(ResponseBase<Tview>));

            try
            {
                _unitOfWork.GetRepo<Tmodel>().Delete(id);
                _unitOfWork.Complete();

                response.Success = true;

                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Exception = ex.ToString();

                return response;
            }

        }

        public Tview GetById(int id)
        {
            Tview view = (Tview)Activator.CreateInstance(typeof(Tview));
            Tmodel model = _unitOfWork.GetRepo<Tmodel>().Get(id);
            if (model == null) return null;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Tmodel, Tview>(); });
            var mapper = config.CreateMapper();
            mapper.Map(model, view);
            return view;
        }

        public List<Tview> FindAllBy(Expression<Func<Tmodel, bool>> predicate, bool orderById = true)
        {
            try
            {
                List<Tview> response = (List<Tview>)Activator.CreateInstance(typeof(List<Tview>));

                IQueryable<Tmodel> query = FindAllByQuery(predicate);
                if (orderById)
                {
                    query = query.OrderBy(l => l.Id);
                }

                foreach (Tmodel model in query)
                {
                    Tview view = (Tview)Activator.CreateInstance(typeof(Tview));

                    mapperModelToView.Map(model, view);

                    if (view == null) throw new Exception("View is null");
                    if (view.Id == null) throw new Exception("view.Id is null");
                    response.Add(view);
                }

                return response;
            }
            catch (Exception ex)
            {
                log.Error("FindAllBy exception occured");
                log.Error(ex.Message);
                log.Error(ex.InnerException);
                log.Error(ex.StackTrace);
                throw ex;
            }

        }

        public IQueryable<Tmodel> FindAllByQuery(Expression<Func<Tmodel, bool>> predicate)
        {
            try
            {
                return _unitOfWork.GetRepo<Tmodel>().Find(predicate);
            }
            catch (Exception ex)
            {
                log.Error("FindAllByQuery exception occured");
                log.Error(ex.Message);
                log.Error(ex.InnerException);
                log.Error(ex.StackTrace);
                throw ex;
            }

        }

        public Tview FindBy(Expression<Func<Tmodel, bool>> predicate)
        {

            Tview view = (Tview)Activator.CreateInstance(typeof(Tview));

            Tmodel model = _unitOfWork.GetRepo<Tmodel>().Find(predicate).FirstOrDefault();
            if (model == null) return null;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Tmodel, Tview>(); });
            var mapper = config.CreateMapper();
            mapper.Map(model, view);

            return view;

        }

        public virtual List<Tview> GetAll()
        {
            List<Tview> response = new List<Tview>();

            //lock (thisLock)
            //{
            IEnumerable<Tmodel> models = _unitOfWork.GetRepo<Tmodel>().GetAll();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Tmodel, Tview>(); });
            var mapper = config.CreateMapper();

            foreach (Tmodel model in models)
            {
                Tview view = (Tview)Activator.CreateInstance(typeof(Tview));
                mapper.Map(model, view);
                response.Add(view);
            }
            //}

            return response;
        }

        public IQueryable<Tmodel> GetAllQuery()
        {
            IQueryable<Tmodel> query = _unitOfWork.GetRepo<Tmodel>().GetAllQuery();
            return query;
        }

        public List<Tview> GetAllSkipTake(int? skip, int? take)
        {
            IQueryable<Tmodel> query = _unitOfWork.GetRepo<Tmodel>().GetAllQuery();
            return GetByFilterResponse(skip, take, query);
        }

        public List<Tview> GetByFilterResponse(int? skip, int? take, IQueryable<Tmodel> query)
        {
            List<Tview> response = new List<Tview>();

            if (!(query is IOrderedQueryable))
            {
                query = query.OrderBy(u => u.Id);
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            //lock (thisLock)
            //{

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Tmodel, Tview>(); });
            var mapper = config.CreateMapper();

            List<Tmodel> models = query.ToList();

            foreach (Tmodel model in models)
            {
                Tview view = (Tview)Activator.CreateInstance(typeof(Tview));
                mapper.Map(model, view);
                response.Add(view);
            }
            //}

            return response;

        }

        public ResponseBase<Tview> InsertOrUpdate(Tview view, bool debug = false)
        {

            try
            {
                ResponseBase<Tview> response = null;

                response = (ResponseBase<Tview>)Activator.CreateInstance(typeof(ResponseBase<Tview>));

                Tmodel model = null;

                model = (Tmodel)Activator.CreateInstance(typeof(Tmodel));

                if (debug)
                {
                    log.Debug("[ServiceBase][InsertOrUpdate] Step03");
                }

                if (view.Id.HasValue)
                {
                    model = _unitOfWork.GetRepo<Tmodel>().Get(view.Id.Value);
                    _unitOfWork.GetRepo<Tmodel>().Update(model);
                    //}
                }
                else
                {
                    _unitOfWork.GetRepo<Tmodel>().Insert(model);

                    if (debug)
                    {
                        log.Debug("[ServiceBase][InsertOrUpdate] Step04");
                    }

                }

                mapperViewToModel.Map(view, model);

                if (debug)
                {
                    log.Debug("[ServiceBase][InsertOrUpdate] Step05");
                }

                _unitOfWork.Complete();

                if (debug)
                {
                    log.Debug("[ServiceBase][InsertOrUpdate] Step06");
                }

                view.Id = model.Id;
                response.Record = view;
                response.Success = true;

                if (debug)
                {
                    log.Debug("[ServiceBase][InsertOrUpdate] Step07");

                    _unitOfWork.DetachAll();

                    log.Debug("[ServiceBase][InsertOrUpdate] Step08");


                }

                return response;

            }
            catch (Exception ex)
            {
                ResponseBase<Tview> response = (ResponseBase<Tview>)Activator.CreateInstance(typeof(ResponseBase<Tview>));
                response.Success = false;
                response.Exception = ex.ToString();
                return response;
            }

        }

        #endregion

        #region async

        public async Task<int> CountAsync(Expression<Func<Tmodel, bool>> predicate)
        {
            return await _unitOfWork.GetRepo<Tmodel>().CountAsync(predicate);
        }

        public async Task<ResponseBase<Tview>> DeleteAsync(int id)
        {

            ResponseBase<Tview> response = (ResponseBase<Tview>)Activator.CreateInstance(typeof(ResponseBase<Tview>));

            try
            {
                await _unitOfWork.GetRepo<Tmodel>().DeleteAsync(id);
                await _unitOfWork.CommitAsync();

                response.Success = true;

                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Exception = ex.ToString();

                return response;
            }

        }

        public async Task<Tview> GetByIdAsync(int id)
        {

            Tview view = (Tview)Activator.CreateInstance(typeof(Tview));
            Tmodel model = await _unitOfWork.GetRepo<Tmodel>().GetAsync(id);

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Tmodel, Tview>(); });
            var mapper = config.CreateMapper();
            mapper.Map(model, view);

            return view;

        }

        public async Task<List<Tview>> FindAllByAsync(Expression<Func<Tmodel, bool>> predicate)
        {
            try
            {
                List<Tview> response = (List<Tview>)Activator.CreateInstance(typeof(List<Tview>));

                IEnumerable<Tmodel> models = await _unitOfWork.GetRepo<Tmodel>().FindAsync(predicate);

                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Tmodel, Tview>(); });
                var mapper = config.CreateMapper();

                foreach (Tmodel model in models)
                {
                    Tview view = (Tview)Activator.CreateInstance(typeof(Tview));
                    mapper.Map(model, view);
                    response.Add(view);
                }
                return response;

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.InnerException);
                log.Error(ex.StackTrace);
                throw ex;
            }

        }

        public async Task<Tview?> FindByAsync(Expression<Func<Tmodel, bool>> predicate)
        {
            Tview view = (Tview)Activator.CreateInstance(typeof(Tview));
            IEnumerable<Tmodel> models = await _unitOfWork.GetRepo<Tmodel>().FindAsync(predicate);
            Tmodel model = models.FirstOrDefault();
            if (model == null) { return null; }
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Tmodel, Tview>(); });
            var mapper = config.CreateMapper();
            mapper.Map(model, view);

            return view;

        }

        public async virtual Task<List<Tview>> GetAllAsync()
        {
            List<Tview> response = new List<Tview>();

            IEnumerable<Tmodel> models = await _unitOfWork.GetRepo<Tmodel>().GetAllAsync();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Tmodel, Tview>(); });
            var mapper = config.CreateMapper();

            foreach (Tmodel model in models)
            {
                Tview view = (Tview)Activator.CreateInstance(typeof(Tview));
                mapper.Map(model, view);
                response.Add(view);
            }

            return response;
        }

        public async Task<List<Tview>> GetAllSkipTakeAsync(int? skip, int? take)
        {
            IQueryable<Tmodel> query = _unitOfWork.GetRepo<Tmodel>().GetAllQuery();
            return await GetByFilterResponseAsync(skip, take, query);
        }

        public async Task<List<Tview>> GetByFilterResponseAsync(int? skip, int? take, IQueryable<Tmodel> query)
        {
            List<Tview> response = new List<Tview>();

            if (!(query is IOrderedQueryable))
            {
                query = query.OrderBy(u => u.Id);
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Tmodel, Tview>(); });
            var mapper = config.CreateMapper();

            List<Tmodel> models = await query.ToListAsync();

            foreach (Tmodel model in models)
            {

                Tview view = (Tview)Activator.CreateInstance(typeof(Tview));
                mapper.Map(model, view);

                response.Add(view);
            }


            return response;

        }

        public async Task<ResponseBase<Tview>> InsertOrUpdateAsync(Tview view)
        {

            ResponseBase<Tview> response = (ResponseBase<Tview>)Activator.CreateInstance(typeof(ResponseBase<Tview>));

            try
            {
                Tmodel model = (Tmodel)Activator.CreateInstance(typeof(Tmodel));

                if (view.Id.HasValue)
                {
                    model = await _unitOfWork.GetRepo<Tmodel>().GetAsync(view.Id.Value);
                    _unitOfWork.GetRepo<Tmodel>().Update(model);
                }
                else
                {
                    _unitOfWork.GetRepo<Tmodel>().Insert(model);
                }

                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Tview, Tmodel>(); });
                var mapper = config.CreateMapper();
                mapper.Map(view, model);
                //model = AutoMap(view);

                await _unitOfWork.CommitAsync();

                //response.Record = AutoMap(model);
                view.Id = model.Id;
                response.Record = view;
                response.Success = true;


                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Exception = ex.ToString();

                return response;
            }

        }

        public async Task<ResponseBase> TruncateAsync()
        {

            ResponseBase response = new();

            try
            {
                await _unitOfWork.GetRepo<Tmodel>().TruncateAsync();
                await _unitOfWork.CommitAsync();

                response.Success = true;

                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Exception = ex.ToString();

                return response;
            }

        }


        #endregion




    }

}
