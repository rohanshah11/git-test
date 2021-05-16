using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class FaqServiceImpl : FaqService
    {
        private readonly FaqRepository _faqRepository ;
        private readonly FaqMaker _faqMaker ;

        public FaqServiceImpl(FaqRepository faqRepository, FaqMaker faqMaker)
        {
            _faqRepository = faqRepository;
            _faqMaker = faqMaker;

        }
        public void delete(long faq_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var faq = _faqRepository.getById(faq_id);
                    if (faq == null)
                    {
                        throw new ItemNotFoundException($" {faq_id}  not found");

                    }
                    _faqRepository.delete(faq);
                    tx.Complete();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void disable(long faq_id)
        {
            try
            {
                var faq = _faqRepository.getById(faq_id);
                if (faq == null)
                    throw new ItemNotFoundException($"Faq with id {faq_id} doesnot exist.");

                faq.is_active = false;
                _faqRepository.update(faq);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void enable(long faq_id)
        {
            try
            {
                var faq = _faqRepository.getById(faq_id);
                if (faq == null)
                    throw new ItemNotFoundException($"Faq with id {faq_id} doesnot exist.");

                faq.is_active = true;
                _faqRepository.update(faq);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void save(FaqDto faqDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Faq faq  = new Faq();
                    _faqMaker.copy(faq, faqDto);
                    _faqRepository.insert(faq);

                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void update(FaqDto faqDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Faq faq  = _faqRepository.getById(faqDto.faq_id);
                    if (faq == null)
                    {
                        throw new ItemNotFoundException($"Faq with ID {faqDto.faq_id} doesnot Exit.");
                    }
                    _faqMaker.copy(faq, faqDto);
                    _faqRepository.update(faq);

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
