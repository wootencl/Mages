﻿namespace Mages.Core.Ast.Expressions
{
    using System;

    sealed class ConditionalExpression : ComputingExpression
    {
        #region Fields

        private readonly IExpression _condition;
        private readonly IExpression _primary;
        private readonly IExpression _secondary;

        #endregion

        #region ctor

        public ConditionalExpression(IExpression condition, IExpression primary, IExpression secondary)
            : base(condition.Start, secondary.End)
        {
            _condition = condition;
            _primary = primary;
            _secondary = secondary;
        }

        #endregion

        #region Properties

        public IExpression Condition 
        {
            get { return _condition; }
        }

        public IExpression Primary 
        {
            get { return _primary; }
        }

        public IExpression Secondary
        {
            get { return _secondary; }
        }

        #endregion

        #region Methods

        public void Validate(IValidationContext context)
        {
            _condition.Validate(context);
            _primary.Validate(context);
            _secondary.Validate(context);
        }

        #endregion
    }
}